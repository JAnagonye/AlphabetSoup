using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("alphabetsoup/_find")]
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
        // POST api/<SearchServiceController> Refer to CouchDBClient for selector, Link = "http://localhost:5984/alphabetsoup/_find", selector
        [HttpPost]
        public void Post([FromBody] string selector)
        {
            
            string selectorJSON = @"{
            ""selector"": {
            ""acronym"": { 
                ""$regex"": " + $"\"{selector}\"" +
                    @"}
                },
            ""fields"": [
            ""_id"",
            ""_rev"",
            ""acronym"", 
            ""fullName"", 
            ""description""
                ]
            }";
            _searchService.Search("G");
        }
    }
}
