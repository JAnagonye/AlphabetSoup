﻿using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModifyServiceController : ControllerBase
    {
        private readonly ILogger<ModifyServiceController> _logger;
        private IModifyService _modifyService;

        public ModifyServiceController(ILogger<ModifyServiceController> logger, IModifyService modify)
        {
            _logger = logger;
            _modifyService = modify;
        }
        // POST api/<ModifyServiceController>/[http://127.0.0.1:5984/database_name/document_id/ -d '{ "field" : "value", "_rev" : "revision id" }'  
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] CouchDBAcronymModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Acronym) || string.IsNullOrWhiteSpace(model.Rev) || string.IsNullOrWhiteSpace(model.Id))
            {
                return BadRequest();
            }
            ICouchDBAcronymModel result = await _modifyService.Edit(model);
            return Ok(result);
        }
    }
}