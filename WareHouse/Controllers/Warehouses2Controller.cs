using Microsoft.AspNetCore.Mvc;
using WareHouse.DTO;
using WareHouse.Repository;

namespace WareHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IWareHouseRepository _wareHouseRepository;

        public Warehouses2Controller(IWareHouseRepository wareHouseRepository)
        {
            _wareHouseRepository = wareHouseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWarehouseProc(ProductWareHouseDTO prodWHDto)
        {
            try
            {
                return Ok(await _wareHouseRepository.AddProdToWHouseProc(prodWHDto));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}