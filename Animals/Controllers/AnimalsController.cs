using Animals.Models;
using Animals.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDBAnimalsService _animalsService;

        public AnimalsController(IDBAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        [Route("/animals")]
        public async Task<IActionResult> GetAnimals(string orderBy)
        {
            IList<Animal> list = await _animalsService.GetAnimalsListAsync(orderBy);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimals(Animal animal)
        {
            try
            {
                await _animalsService.AddAnimals(animal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(await _animalsService.AddAnimals(animal));
        }
    }
}