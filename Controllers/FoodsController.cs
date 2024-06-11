using Microsoft.AspNetCore.Mvc;
using WebScrapping_C.Data;
using Microsoft.EntityFrameworkCore;

namespace WebScrapping_C.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodsController : ControllerBase
{
    private readonly ILogger<FoodsController> _logger;
    public FoodsController(ILogger<FoodsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("skip/{skip:int}/take/{take:int}")]
    public async Task<IActionResult> GetAsync(
        [FromServices]FoodsContex contex,
        [FromRoute] int skip = 0,
        [FromRoute] int take = 25)
    {
        var items = await contex
            .Items
            .Include(i => i.Details)
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return Ok(items);
    }
}
