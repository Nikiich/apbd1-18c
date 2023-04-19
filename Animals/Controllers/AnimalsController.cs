using System.Net;
using Animals.DTO;
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
            try
            {
                IList<Animal> list = await _animalsService.GetAnimalsListAsync(orderBy);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimals(AnimalDTO animal)
        {
            try
            {
                var res = await _animalsService.AddAnimals(animal);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("/{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal(AnimalDTO animal, int idAnimal)
        {
            try
            {
                var res = await _animalsService.UpdateAnimals(animal, idAnimal);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("/{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            bool res = await _animalsService.DeleteAnimal(idAnimal);
            if (res)
            {
                return Ok($"Deleted : id = {idAnimal} ");
            }

            return BadRequest($"Animal ID {idAnimal} does not exist");
        }
    }
}