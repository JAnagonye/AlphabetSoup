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
        private readonly ILogger<SearchServiceController> _logger;
        private ISearchService _searchService;

        public SearchServiceController(ILogger<SearchServiceController> logger, ISearchService search)
        {
            _logger = logger;
            _searchService = search;
        }

        // GET: api/<SearchServiceController>
        [HttpGet("{acronymSearch}")]
        public IActionResult Get(string acronymSearch)
        {
            if(acronymSearch == null)
            {
                return BadRequest();
            }
            ICouchDBDocsModel result = _searchService.Search(acronymSearch);
            return Ok(result);
        }
    }
}
