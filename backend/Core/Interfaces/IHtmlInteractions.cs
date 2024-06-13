using HtmlAgilityPack;
namespace WebScrapping_C.Core.Interfaces
{
    public interface IHtmlInteractions
    {
        public string HtmlDecoded(string html);
        public Task<HtmlNodeCollection> GetElementsNodeAsync(string page, string url, string query, string tag);
        public HtmlNodeCollection SelectElements(string innerHtml, string tag);
    }
}
