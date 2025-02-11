using System.Security.Principal;
using DnsClient;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    /* Using ActionResult instead of IActionResult because it
     gives us type safety */
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm)
    {
        var query = DB.Find<Item>();

        query.Sort(x => x.Make, Order.Ascending);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }
        
        var result = await query.ExecuteAsync();

        return result;
    }
}