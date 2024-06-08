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


        while (hasNextPage)
        {
            var web = new HtmlWeb();
            HtmlDocument document = web.Load($"https://www.tbca.net.br/base-dados/composicao_estatistica.php?pagina={page}&atuald=1");
            var nodesList = document.DocumentNode.SelectNodes("//table/tbody/tr");


            if (nodesList != null)
            {

                foreach(var node in nodesList)
                {
                    List<string> arr = new List<string>();
                    foreach (var item in node.ChildNodes)
                    {
                        string encodedData = item.InnerText;
                        string decodedData = HttpUtility.HtmlDecode(encodedData);
                        arr.Add(decodedData);
                    }
                    var nodes = arr.ToArray();
                    var objItem = new Item();
                    objItem.Code = nodes[0];
                    objItem.Name = nodes[1];
                    objItem.ScientificName = nodes[2];
                    objItem.Group = nodes[3];
                    objItem.Brand = nodes[4];
                    items.Add(objItem);

                }
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
