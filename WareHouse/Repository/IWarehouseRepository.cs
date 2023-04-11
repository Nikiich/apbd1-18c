using WareHouse.DTO;

namespace WareHouse.Repository
{
    public interface IWareHouseRepository
    {
        public Task<int> AddProdToWHouse(ProductWareHouseDTO productWhDTO);
        public Task<int> AddProdToWHouseProc(ProductWareHouseDTO productWhDTO);
    }
}
