﻿using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurgeServiceController : ControllerBase
    {
        private readonly ILogger<PurgeServiceController> _logger;
        private IPurgeService _purgeService;

        public PurgeServiceController(ILogger<PurgeServiceController> logger, IPurgeService purge)
        {
            _logger = logger;
            _purgeService = purge;
        }
        // Delete api/<PurgeServiceController> Refer to CouchDB for PurgeJson Link = "http://localhost:5984/alphabetsoup/_purge", purge 
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody]PurgeModel purgeModel)
        {
            if (purgeModel.Id == null || purgeModel.Rev == null)
            {
                return BadRequest();
            }
            IPurgeResponse response = await _purgeService.Delete(purgeModel);
            return Ok(response);
        }
    }
}
