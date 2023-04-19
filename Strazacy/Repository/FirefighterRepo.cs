using Strazacy.Models;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Strazacy.Repository
{
    public class FirefighterRepo : IFirefightersRepo
    {
        private readonly IConfiguration _configuration;

        public FirefighterRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<MAction> DeleteActionAsync(int idAction)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new();
            string query = $"SELECT * FROM Action WHERE IdAction = {idAction}";
            
            command.Connection= conn;
            command.CommandText= query;
            conn.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if(!await reader.ReadAsync()) {
                throw new Exception($"Action with id {idAction} does not exists");
            }
            DateTime datetime;
            bool b = DateTime.TryParse(reader["EndTime"].ToString(), out datetime);
            MAction action = new MAction
            {
                IdAction = idAction,
                StartTime = DateTime.Parse(reader["StartTime"].ToString(), CultureInfo.InvariantCulture),
                NeedSpecialEquipment = reader["NeedSpecialEquipment"].ToString().Equals(1) ? true : false,
                EndTime = b ? datetime : null

            };
            if (action.EndTime != null) {
                throw new Exception("Can not delete action with ENDTIME");
            }

            query = $"DELETE Action WHERE IdAction = {idAction}";
            command.CommandText = query;
            await command.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return action;

        }

        public async Task<MAction> GetActionInfoAsync(int idAction)
        {
            using SqlConnection conn = 
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new();
            string query = "SELECT a.StartTime, a.EndTime, a.NeedSpecialEquipment,  fa.IdFirefighter, f.FirstName, f.LastName FROM Firefighter_Action fa JOIN Action a ON a.IdAction = fa.IdAction " +
                "JOIN Firefighter f ON f.IdFirefighter = fa.IdFirefighter" +
                $" WHERE fa.IdAction = {idAction}";
            command.CommandText = query;
            command.Connection= conn;
            await conn.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows) 
            {
                throw new Exception($"action with id {idAction} not found");
            }
            bool flag = true;
            MAction action = new ();
            while(await reader.ReadAsync()) { 
                if (flag)
                {
                    DateTime datetime;
                    bool b = DateTime.TryParse(reader["EndTime"].ToString(), out datetime);
                    action = new MAction
                    {
                        IdAction = idAction,
                        StartTime = DateTime.Parse(reader["StartTime"].ToString(), CultureInfo.InvariantCulture),
                        NeedSpecialEquipment = reader["NeedSpecialEquipment"].ToString().Equals(1) ? true : false,
                        EndTime = b ? datetime : null

                    };
                   
                    flag = false;
                }
                action.Firefighters.Add(new Firefighter
                {
                    IdFirefighter = int.Parse(reader["IdFirefighter"].ToString()),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString()
                });
            }
            await conn.CloseAsync();
            return action;
        }

        
    }
}
