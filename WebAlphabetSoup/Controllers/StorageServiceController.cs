﻿using AlphabetSoup.Models;
using AlphabetSoup.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAlphabetSoup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageServiceController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly ILogger<StorageServiceController> _logger;

        public StorageServiceController(ILogger<StorageServiceController> logger, IStorageService store)
        {
            _storageService = store;
            _logger = logger;

        }
        // POST api/<StorageServiceController> Link = $"http://localhost:5984/alphabetsoup/{g}"
        [HttpPost("{acronym}, {fullName}, {desc}")]
        public IActionResult Post(string acronym, string fullName, string desc)
        {
            Task<ICouchDBAcronymModel> result = _storageService.Store(acronym, fullName, desc);
            return Ok(result);
        }
    }
}
