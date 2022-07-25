using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModifyServiceController : ControllerBase
    {
        private IModifyService _modifyService;

        public ModifyServiceController(IModifyService modify)
        {
            _modifyService = modify;
        }
        // POST api/<ModifyServiceController>/[http://127.0.0.1:5984/database_name/document_id/ -d '{ "field" : "value", "_rev" : "revision id" }'  
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CouchDBAcronymModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(model.Acronym) || string.IsNullOrWhiteSpace(model.Rev) || string.IsNullOrWhiteSpace(model.Id))
            {
                return BadRequest();
            }
            ICouchDBAcronymModel result = await _modifyService.Edit(model);
            return Ok(result);
        }
    }
}
