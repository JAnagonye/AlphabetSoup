using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurgeServiceController : ControllerBase
    {
        private IPurgeService _purgeService;

        public PurgeServiceController(IPurgeService purge)
        {
            _purgeService = purge;
        }
        // Delete api/<PurgeServiceController> Refer to CouchDB for PurgeJson Link = "http://localhost:5984/alphabetsoup/_purge", purge 
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody]PurgeModel purgeModel)
        {
            if (purgeModel == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(purgeModel.Rev) || string.IsNullOrWhiteSpace(purgeModel.Id))
            {
                return BadRequest();
            }
            IPurgeResponse response = await _purgeService.Delete(purgeModel);
            return Ok(response);
        }
    }
}
