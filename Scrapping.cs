using System.Web;
using HtmlAgilityPack;

enum EnergyKJ
{
    Units = 1,
    ValuePer = 2,
    Standard = 3,
    Minimum = 4,
    Maximum = 5,
    NumberOfDataUsed = 6,
    References = 7,
    DataType = 8,
}

namespace WebScrapping_C
{
    public class Scrapping
    {

        private HtmlDocument LoadHtmlDocument(string page, string url, string query)
        {
            var web = new HtmlWeb();
            string urlFormat = string.Format(url + query, page);
            return web.Load(urlFormat);
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

        private Item AddDatails(Item item)
        {
            var url = "https://www.tbca.net.br/base-dados/int_composicao_estatistica.php";
            var query = "?cod_produto={0}";
            var code = item.Code;
            HtmlDocument htmlDocument = LoadHtmlDocument(code, url, query);
            var nodesList = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr/td");


            if (nodesList != null)
            {
                var indxUnits = (int)EnergyKJ.Units;
                item.Details.EnergyKJ.Units = HtmlDecoded(nodesList[indxUnits].InnerText);

                var indxValuePer = (int)EnergyKJ.ValuePer;
                item.Details.EnergyKJ.ValuePer100G = HtmlDecoded(nodesList[indxValuePer].InnerText);

                var indxStandard = (int)EnergyKJ.Standard;
                item.Details.EnergyKJ.StandardDeviation = HtmlDecoded(nodesList[indxStandard].InnerText);

                var indxMinimum = (int)EnergyKJ.Minimum;
                item.Details.EnergyKJ.MinimumValue = HtmlDecoded(nodesList[indxMinimum].InnerText);

                var indxMaximum = (int)EnergyKJ.Maximum;
                item.Details.EnergyKJ.MaximumValue = HtmlDecoded(nodesList[indxMaximum].InnerText);

                var indxNumberOfDataUsed = (int)EnergyKJ.NumberOfDataUsed;
                item.Details.EnergyKJ.NumberOfDataUsed = HtmlDecoded(nodesList[indxNumberOfDataUsed].InnerText);

                var indxReferences = (int)EnergyKJ.References;
                item.Details.EnergyKJ.References = HtmlDecoded(nodesList[indxReferences].InnerText);

                var indxType = (int)EnergyKJ.DataType;
                item.Details.EnergyKJ.DataType = HtmlDecoded(nodesList[indxType].InnerText);
            }

            return item;
        }

        // https://www.tbca.net.br/base-dados/int_composicao_estatistica.php ?cod_produto=BRC0001C //details
        public List<Item> Execute()
        {
            int page = 1;
            List<Item> items = new List<Item>();
            bool hasNextPage = true;

            while (hasNextPage)
            {
                var url = "https://www.tbca.net.br/base-dados/composicao_estatistica.php";
                var query = "?pagina={0}&atuald=1";
                HtmlDocument htmlDocument = LoadHtmlDocument(page.ToString(), url, query);
                var nodesList = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");

                if (nodesList != null)
                {
                    var newItems = nodesList.Select(InstanceItem).ToList();

                    newItems.ForEach((x) => { AddDatails(x); });
                    items.AddRange(newItems);
                    page++;
                }
                else
                {

                    hasNextPage = false;
                }
            }
            return items;
        }
    }
}
