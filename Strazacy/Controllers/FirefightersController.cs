using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strazacy.Repository;

namespace Strazacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirefightersController : ControllerBase
    {
        private readonly IFirefightersRepo _repo;

        public FirefightersController(IFirefightersRepo repo)
        {
            _repo = repo;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAction(int idAction) {
            try
            {
                var res = await _repo.DeleteActionAsync(idAction);
                return Ok(res);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        } 

        [HttpGet]
        public async Task<IActionResult> GetActionInfoAsync(int idAction)
        {
            var res = await _repo.GetActionInfoAsync(idAction);
            return Ok(res);
        }
    }
}
