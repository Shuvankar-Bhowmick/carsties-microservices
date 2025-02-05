using Microsoft.AspNetCore.Mvc;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    /* Using ActionResult instead of IActionResult because it
     gives us type safety */
    [HttpGet]
    public async Task<ActionResult<Item>> SearchItems(string searchTerm)
    {
        /* TODO: Write code for SearchItems action method */
        return await Task.FromResult<ActionResult<Item>>(Ok(new Item()));
    }
}