using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Web;

namespace WebScrapping_C.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<FoodsController> _logger;

    public FoodsController(ILogger<FoodsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<FoodsMock> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new FoodsMock
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
