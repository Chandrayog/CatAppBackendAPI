using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CatApi.Services;
using CatAPI.DTO;

namespace CatApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catService;

        public CatController(ICatService catService)
        {
            _catService = catService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomCat()
        {
            try
            {
                CatData catData = await _catService.GetRandomCatDataAsync();
                return Ok(catData);
            }
            catch (Exception)
            {
                // Optionally log the exception here
                return StatusCode(500, new { error = "An error occurred while fetching cat data." });
            }
        }
    }
}