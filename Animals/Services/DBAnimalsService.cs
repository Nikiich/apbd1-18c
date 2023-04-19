using Animals.DTO;
using Animals.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace Animals.Services
{
    public class DbAnimalsService : IDBAnimalsService
    {
        private readonly IConfiguration _configuration;

        public DbAnimalsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IList<Animal>> GetAnimalsListAsync(string orderBy)
        {
            await using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new();
            List<Animal> list = new List<Animal>();
            string sqlQuery;
            if (orderBy.IsNullOrEmpty())
            {
                sqlQuery = "SELECT * FROM Animal ORDER BY Name ASC ";
            }
            else
            {
                sqlQuery = orderBy.ToLower() switch
                {
                    "name" => "SELECT * FROM Animal ORDER BY Name ASC ",
                    "description" => "SELECT * FROM Animal ORDER BY Description ASC",
                    "category" => "SELECT * FROM Animal ORDER BY Category ASC",
                    "area" => "SELECT * FROM Animal ORDER BY Area ASC",
                    _ => throw new Exception("invalid parameter")
                };
            }

            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            await conn.OpenAsync();
            await using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Animal anim = new Animal()
                {
                    IdAnimal = int.Parse(reader["IdAnimal"].ToString()),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Category = reader["Category"].ToString(),
                    Area = reader["Area"].ToString(),
                };
                list.Add(anim);
            }

            await conn.CloseAsync();
            return list;
        }

        public async Task<Animal> AddAnimals(AnimalDTO animal)
        {
            await using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new();
            string sqlQuery = 
                              "INSERT INTO Animal(Name, Description, Category, Area) OUTPUT Inserted.IdAnimal VALUES (@Name, @Description, @Category, @Area )";
            // cmd.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
            cmd.Parameters.AddWithValue("Name", animal.Name);
            cmd.Parameters.AddWithValue("Category", animal.Category);
            cmd.Parameters.AddWithValue("Description", animal.Description);
            cmd.Parameters.AddWithValue("Area", animal.Area);
            cmd.Connection = conn;
            cmd.CommandText = sqlQuery;
            await conn.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            await reader.ReadAsync();
            if (!reader.HasRows)
            {
                throw new Exception("Invalid data");
            }

            int pk = int.Parse(reader["IdAnimal"].ToString());
            await conn.CloseAsync();
            return new Animal(animal, pk);
        }

        public async Task<Animal> UpdateAnimals(AnimalDTO animal, int idAnimal)
        {
            await using SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new();
            string sqlQuery =
                "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
            command.Parameters.AddWithValue("@IdAnimal", idAnimal);
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", animal.Description);
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);
            command.CommandText = sqlQuery;
            command.Connection = connection;
            await connection.OpenAsync();
            int res = command.ExecuteNonQuery();
            if (res == 0)
            {
                throw new Exception($"Animal ID {idAnimal} does not exist");
            }

            await connection.CloseAsync();
            return new Animal(animal, idAnimal);
        }

        public async Task<bool> DeleteAnimal(int idAnimal)
        {
            await using SqlConnection
                conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand();
            string sqlQuery = $"DELETE Animal WHERE IdAnimal = {idAnimal}";
            command.CommandText = sqlQuery;
            command.Connection = conn;
            await conn.OpenAsync();
            int res = command.ExecuteNonQuery();
            if (res < 1)
            {
                return false;
            }

            return true;
        }
    }
}