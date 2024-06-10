using System.Web;
using HtmlAgilityPack;

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

namespace WebScrapping_C
{
    public class Scrapping
    {

        private async Task<HtmlDocument> LoadHtmlDocumentAsync(string page, string url, string query)
        {
            var web = new HtmlWeb();
            string urlFormat = string.Format(url + query, page);
            return await web.LoadFromWebAsync(urlFormat);
        }

        private string HtmlDecoded(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        private Item InstanceItem(HtmlNode node)
        {
            var item = new Item();
            var child = node.ChildNodes;
            item.Code = HtmlDecoded(child[0].InnerText);
            item.Name = HtmlDecoded(child[1].InnerText);
            item.ScientificName = HtmlDecoded(child[2].InnerText);
            item.Group = HtmlDecoded(child[3].InnerText);
            item.Brand = HtmlDecoded(child[4].InnerText);
            return item;
        }

        private async Task<Item> AddDetails(Item item)
        {
            var BaseUrl = "https://www.tbca.net.br/base-dados/int_composicao_estatistica.php";
            var query = "?cod_produto={0}";
            var code = item.Code;
            HtmlDocument htmlDocument = await LoadHtmlDocumentAsync(code, BaseUrl, query);
            var rows = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");

            const int componentIndx = (int)EnumProperties.Component;
            const int UnitsIndx = (int)EnumProperties.Units;
            const int ValuePerIndx = (int)EnumProperties.ValuePer;
            const int StandardIndx = (int)EnumProperties.Standard;
            const int MinimumIndx = (int)EnumProperties.Minimum;
            const int MaximumIndx = (int)EnumProperties.Maximum;
            const int NumberOfDataIndx = (int)EnumProperties.NumberOfDataUsed;
            const int ReferenceIndx = (int)EnumProperties.References;
            const int DataTypeIndx = (int)EnumProperties.DataType;

            foreach (var row in rows)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(row.InnerHtml);
                var td = doc.DocumentNode.SelectNodes("/td");

                var properties = new Properties
                {
                    Component = HtmlDecoded(td[componentIndx].InnerText),
                    Units = HtmlDecoded(td[UnitsIndx].InnerText),
                    ValuePer100G = HtmlDecoded(td[ValuePerIndx].InnerText),
                    StandardDeviation = HtmlDecoded(td[StandardIndx].InnerText),
                    MinimumValue = HtmlDecoded(td[MinimumIndx].InnerText),
                    MaximumValue = HtmlDecoded(td[MaximumIndx].InnerText),
                    NumberOfDataUsed = HtmlDecoded(td[NumberOfDataIndx].InnerText),
                    References = HtmlDecoded(td[ReferenceIndx].InnerText),
                    DataType = HtmlDecoded(td[DataTypeIndx].InnerText)
                };
                item.Details.Add(properties);
            }

            return item;
        }

        public async Task<List<Item>> ExecuteAsync()
        {
            int page = 1;
            List<Item> items = new List<Item>();

            while (true)
            {
                Console.WriteLine(page);
                var BaseUrl = "https://www.tbca.net.br/base-dados/composicao_estatistica.php";
                var query = "?pagina={0}&atuald=1";
                HtmlDocument htmlDocument = await LoadHtmlDocumentAsync(page.ToString(), BaseUrl, query);
                var nodesList = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");

                if (nodesList != null)
                {
                    var newItems = nodesList.Select(InstanceItem).ToList();

                    foreach (var item in newItems) {
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
            return items;
        }
    }
}
