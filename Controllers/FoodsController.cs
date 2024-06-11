using Microsoft.AspNetCore.Mvc;
using WebScrapping_C.Data;
using Microsoft.EntityFrameworkCore;

namespace WebScrapping_C.Controllers;

[ApiController]
[Route("[controller]")]
public class FoodsController : ControllerBase
{
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

    [HttpGet("filter", Name = "FilterFoodsByName")]
    public async Task<IActionResult> FilterByNameAsync(
        [FromServices] FoodsContex contex,
        [FromQuery] string name,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 25)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("The 'name' query parameter is required.");
        }

        var items = await contex.Items
            .Include(i => i.Details)
            .Where(i => i.Name.Contains(name))
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();

        return Ok(items);
    }
}
