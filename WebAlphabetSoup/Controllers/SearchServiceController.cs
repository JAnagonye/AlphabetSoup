using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlphabetSoup.Models;
using AlphabetSoup.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchServiceController : ControllerBase
    {
        private ISearchService _searchService;

        public SearchServiceController(ISearchService search)
        {
            _searchService = search;
        }

        // GET: api/<SearchServiceController>
        [HttpGet("{acronymSearch}")]
        public async Task<IActionResult> GetAsync(string acronymSearch)
        {
            if(string.IsNullOrWhiteSpace(acronymSearch))
            {
                return BadRequest();
            }
            ICouchDBDocsModel result = await _searchService.Search(acronymSearch);
            return Ok(result);
        }
    }
}
