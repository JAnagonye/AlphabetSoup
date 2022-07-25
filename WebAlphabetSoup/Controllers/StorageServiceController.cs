using AlphabetSoup.Models;
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

        public StorageServiceController(IStorageService store)
        {
            _storageService = store;

        }
        // POST api/<StorageServiceController> Link = $"http://localhost:5984/alphabetsoup/{g}"
        [HttpPost("{acronym}, {fullName}, {desc}")]
        public async Task<IActionResult> PostAsync(string acronym, string fullName, string desc)
        {
            if (string.IsNullOrWhiteSpace(acronym) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(desc))
            {
                return BadRequest();
            }
            ICouchDBAcronymModel result = await _storageService.Store(acronym, fullName, desc);
            return Ok(result);
        }
    }
}
