using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using WareHouse.DTO;
using WareHouse.Exceptions;
using WareHouse.Models;

namespace WareHouse.Repository
{
    public class WarehouseRepository : IWareHouseRepository
    {
        public WarehouseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public async Task<int> AddProdToWHouse(ProductWareHouseDTO productWhDTO)
        {
            SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            SqlCommand command = new();
            string query = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
            command.Parameters.AddWithValue("IdProduct", productWhDTO.IdProduct);
            command.CommandText = query;
            command.Connection = connection;
            await connection.OpenAsync();
            SqlDataReader read = await command.ExecuteReaderAsync();
            if (!await read.ReadAsync())
            {
                throw new Exception($"Product with id {productWhDTO.IdProduct} does not exist");
            }

            if (productWhDTO.Amount <= 0)
            {
                throw new Exception($"Incorrect amount <{productWhDTO.Amount}>");
            }

            Product pr = new Product()
            {
                IdProduct = int.Parse(read["IdProduct"].ToString()),
                Name = read["Name"].ToString(),
                Description = read["Description"].ToString(),
                Price = double.Parse(read["Price"].ToString())
            };
            Console.WriteLine(pr);
            await read.CloseAsync();
            await connection.CloseAsync();
            command.Parameters.Clear();
            query =
                $"SELECT * FROM \"Order\" WHERE IdProduct = {pr.IdProduct} AND Amount = {productWhDTO.Amount} AND CreatedAt < '{productWhDTO.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
            command.CommandText = query;
            await connection.OpenAsync();
            read = await command.ExecuteReaderAsync();
            if (!await read.ReadAsync())
            {
                throw new Exception(
                    $"Order with product id <{pr.IdProduct}> and amount <{productWhDTO.Amount}> and CreatedAt <{productWhDTO.CreatedAt}> not found");
            }

            Order order = new Order()
            {
                IdOrder = int.Parse(read["IdOrder"].ToString()),
                Amount = int.Parse(read["Amount"].ToString()),
                CreatedAt = DateTime.Parse(read["CreatedAt"].ToString()),
                IdProduct = int.Parse(read["IdProduct"].ToString())
            };
            await read.CloseAsync();

            query = $"SELECT * FROM Product_Warehouse WHERE IdOrder = {order.IdOrder}";
            command.CommandText = query;
            read = await command.ExecuteReaderAsync();
            await read.ReadAsync();
            if (read.HasRows)
            {
                throw new AlreadyCompletedException($"Order with id <{order.IdOrder}> already was accepted");
            }

            await read.CloseAsync();

            query =
                $"UPDATE \"Order\" SET FulfilledAt = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE IdOrder = {order.IdOrder}";
            command.CommandText = query;
            await command.ExecuteNonQueryAsync();
            query = "INSERT INTO Product_WareHouse(IdWareHouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                    $"VALUES ({productWhDTO.IdWareHouse}, {pr.IdProduct}, {order.IdOrder}, {productWhDTO.Amount}, {(productWhDTO.Amount * pr.Price).ToString("F5", CultureInfo.InvariantCulture)}, '{productWhDTO.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
            Console.WriteLine(query);
            command.CommandText = query;
            await command.ExecuteNonQueryAsync();
            query = $"SELECT IdProductWarehouse FROM Product_Warehouse WHERE IdOrder = {order.IdOrder}";
            command.CommandText = query;
            read = await command.ExecuteReaderAsync();
            await read.ReadAsync();
            int idProdWareHouse = int.Parse(read["IdProductWarehouse"].ToString());
            await read.CloseAsync();
            await connection.CloseAsync();

            return idProdWareHouse;
        }

        public async Task<int> AddProdToWHouseProc(ProductWareHouseDTO productWhDTO)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("AddProductToWarehouse", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdProduct", productWhDTO.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", productWhDTO.IdWareHouse);
            command.Parameters.AddWithValue("@Amount", productWhDTO.Amount);
            command.Parameters.AddWithValue("@CreatedAt", productWhDTO.CreatedAt);
            await connection.OpenAsync();
            try
            {
                await command.ExecuteNonQueryAsync();
                string query =
                    $"SELECT IdProductWarehouse FROM Product_Warehouse WHERE IdProduct = @IdProduct AND CreatedAt = @CreatedAt AND IdWarehouse = @IdWarehouse AND Amount = @Amount";
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    int res = int.Parse(reader["IdProductWarehouse"].ToString());
                    await connection.CloseAsync();
                    return res;
                }
            }
            catch (Exception e)
            {
                await connection.CloseAsync();
                throw;
            }
        }
    }
}