using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurgeServiceController : ControllerBase
    {

        // POST api/<PurgeServiceController> Refer to CouchDB for PurgeJson Link = "http://localhost:5984/alphabetsoup/_purge", purge 
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
