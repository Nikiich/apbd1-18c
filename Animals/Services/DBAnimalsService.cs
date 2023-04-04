using Animals.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace Animals.Services
{
    public class DBAnimalsService : IDBAnimalsService
    {
        private readonly IConfiguration _configuration;

        public DBAnimalsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IList<Animal>> GetAnimalsListAsync(string orderBy)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new();
            List<Animal> list = new List<Animal>();
            string sqlQuery = "";
            if (orderBy.IsNullOrEmpty())
            {
                sqlQuery = "SELECT * FROM Animal ORDER BY Name ASC ";
            }
            else
            {
                sqlQuery = "SELECT * FROM Animal ORDER BY @Order ASC ";

                cmd.Parameters.AddWithValue("@Order", "Description");

            }
            
            cmd.CommandText = sqlQuery;
            Console.WriteLine(cmd.CommandText);
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

        public async Task<Animal> AddAnimals(Animal animal)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new();
            string sqlQuery ="SET IDENTITY_INSERT Animal ON; "+
                "INSERT INTO Animal(IdAnimal, Name, Description, Category, Area) VALUES (@IdAnimal, @Name, @Description, @Category, @Area )";
            cmd.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
            cmd.Parameters.AddWithValue("Name", animal.Name);
            cmd.Parameters.AddWithValue("Category", animal.Category);
            cmd.Parameters.AddWithValue("Description", animal.Description);
            cmd.Parameters.AddWithValue("Area", animal.Area);
            cmd.Connection = conn;
            cmd.CommandText = sqlQuery;
            await conn.OpenAsync();
            if (cmd.ExecuteNonQuery() < 1)
            {
                throw new Exception("Invalid data");
            }
            await conn.CloseAsync();
            return animal;
        }
    }
}