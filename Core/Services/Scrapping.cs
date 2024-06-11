using System.Web;
using HtmlAgilityPack;
using WebScrapping_C.Core.Interfaces;
using WebScrapping_C.Model;
using WebScrapping_C.Repository;

enum EnumProperties
{
    Component,
    Units,
    ValuePer,
    Standard,
    Minimum,
    Maximum,
    NumberOfDataUsed,
    References,
    DataType,
}

namespace WebScrapping_C.Core.Services
{
    public class Scrapping
    {
        private readonly IFoodsRepository repository;
        private readonly IHtmlInteractions htmlScrapping;

        public Scrapping(IFoodsRepository repository, IHtmlInteractions htmlScrapping)
        {
            this.repository = repository;
            this.htmlScrapping = htmlScrapping;
        }

        private Item InstanceItem(HtmlNode node)
        {
            var item = new Item();
            var child = node.ChildNodes;
            item.Code = this.htmlScrapping.HtmlDecoded(child[0].InnerText);
            item.Name = this.htmlScrapping.HtmlDecoded(child[1].InnerText);
            item.ScientificName = this.htmlScrapping.HtmlDecoded(child[2].InnerText);
            item.Group = this.htmlScrapping.HtmlDecoded(child[3].InnerText);
            item.Brand = this.htmlScrapping.HtmlDecoded(child[4].InnerText);
            return item;
        }

        private async Task<Item> AddDetails(Item item)
        {
            const string BaseUrl = "https://www.tbca.net.br/base-dados/int_composicao_estatistica.php";
            const string query = "?cod_produto={0}";
            var code = item.Code;
            const string tag = "//table/tbody/tr";

            var Elements = await this.htmlScrapping.GetElementsNodeAsync(code, BaseUrl, query, tag);

            const int componentIndx = (int)EnumProperties.Component;
            const int UnitsIndx = (int)EnumProperties.Units;
            const int ValuePerIndx = (int)EnumProperties.ValuePer;
            const int StandardIndx = (int)EnumProperties.Standard;
            const int MinimumIndx = (int)EnumProperties.Minimum;
            const int MaximumIndx = (int)EnumProperties.Maximum;
            const int NumberOfDataIndx = (int)EnumProperties.NumberOfDataUsed;
            const int ReferenceIndx = (int)EnumProperties.References;
            const int DataTypeIndx = (int)EnumProperties.DataType;

            foreach (var row in Elements)
            {
                const string tagElement = "/td";
                var td = this.htmlScrapping.SelectElements(row.InnerHtml, tagElement);

                var properties = new Properties
                {
                    Component = this.htmlScrapping.HtmlDecoded(td[componentIndx].InnerText),
                    Units = this.htmlScrapping.HtmlDecoded(td[UnitsIndx].InnerText),
                    ValuePer100G = this.htmlScrapping.HtmlDecoded(td[ValuePerIndx].InnerText),
                    StandardDeviation = this.htmlScrapping.HtmlDecoded(td[StandardIndx].InnerText),
                    MinimumValue = this.htmlScrapping.HtmlDecoded(td[MinimumIndx].InnerText),
                    MaximumValue = this.htmlScrapping.HtmlDecoded(td[MaximumIndx].InnerText),
                    NumberOfDataUsed = this.htmlScrapping.HtmlDecoded(td[NumberOfDataIndx].InnerText),
                    References = this.htmlScrapping.HtmlDecoded(td[ReferenceIndx].InnerText),
                    DataType = this.htmlScrapping.HtmlDecoded(td[DataTypeIndx].InnerText)
                };
                item.Details.Add(properties);
            }

            await repository.CreateItemAsync(item);
            bool result = await repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Failed to save.");
            }

            return item;
        }

        public async Task ExecuteAsync()
        {
            int page = 1;
            List<Item> items = new List<Item>();

            while (true)
            {
                const string BaseUrl = "https://www.tbca.net.br/base-dados/composicao_estatistica.php";
                const string query = "?pagina={0}&atuald=1";
                const string tag = "//table/tbody/tr";
                var Elements = await this.htmlScrapping.GetElementsNodeAsync(page.ToString(), BaseUrl, query, tag);

                if (Elements != null)
                {
                    var newItems = Elements.Select(InstanceItem).ToList();

                    foreach (var item in newItems)
                    {
                        await AddDetails(item);
                    }
                    items.AddRange(newItems);
                    page++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
