using System.Web;
using HtmlAgilityPack;
using WebScrapping_C.Core.Interfaces;

namespace WebScrapping_C.Core.Services
{
    public class HtmInteractions : IHtmlInteractions
    {
        public string HtmlDecoded(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        public async Task<HtmlNodeCollection> GetElementsNodeAsync(string page, string url, string query, string tag)
        {
            var web = new HtmlWeb();
            string urlFormat = string.Format(url + query, page);
            var htmlDocument = await web.LoadFromWebAsync(urlFormat);
            var Elements = htmlDocument.DocumentNode.SelectNodes(tag);

            return Elements;
        }

        public HtmlNodeCollection SelectElements(string innerHtml, string tag)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(innerHtml);
            var td = doc.DocumentNode.SelectNodes(tag);
            return td;
        }

    }
}
