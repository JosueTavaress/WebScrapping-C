using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Web;

namespace WebScrapping_C.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        int page = 1;
        List<Item> items = new List<Item>();
        bool hasNextPage = true;

        HtmlDocument LoadHtmlDocument(int page)
        {
            string BaseUrl = "https://www.tbca.net.br/base-dados/composicao_estatistica.php";
            string Query = "?pagina={0}&atuald=1";
            var web = new HtmlWeb();
            string url = string.Format(BaseUrl + Query, page);
            return web.Load(url);
        }

        string HtmlDecoded(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        Item InstanceItem(HtmlNode node)
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

        while (hasNextPage)
        {
            HtmlDocument htmlDocument = LoadHtmlDocument(page);
            var nodesList = htmlDocument.DocumentNode.SelectNodes("//table/tbody/tr");

            if (nodesList != null)
            {
                var newItems = nodesList.Select(InstanceItem).ToList();
                items.AddRange(newItems);
                page++;
            }
            else {
                hasNextPage = false;
            }
        }
       
        foreach (var item in items)
        {
            Console.WriteLine($"Code: {item.Code}, Name: {item.Name}, ScientificName: {item.ScientificName}, Group: {item.Group}, Brand: {item.Brand}");
        }
       

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
