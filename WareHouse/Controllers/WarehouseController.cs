using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHouse.DTO;
using WareHouse.Exceptions;
using WareHouse.Repository;

namespace WareHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseRepository _wareHouseRepository;

        public WareHouseController(IWareHouseRepository wareHouseRepository)
        {
            _wareHouseRepository = wareHouseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWareHouse(ProductWareHouseDTO product)
        {
            try
            {
                int res = await _wareHouseRepository.AddProdToWHouse(product);
                return Ok(res);
            }
            catch (AlreadyCompletedException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}