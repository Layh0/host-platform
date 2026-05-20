using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;
using practika_2.Core;

namespace practika_2.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<HostingPlan>> GetHostingPlansAsync()
        {
            var plans = new List<HostingPlan>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT * FROM HostingPlans", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                plans.Add(new HostingPlan
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    DiskSpaceGB = reader.GetInt32(reader.GetOrdinal("DiskSpaceGB")),
                    SiteLimit = reader.GetInt32(reader.GetOrdinal("SiteLimit"))
                });
            }
            return plans;
        }

        public async Task<List<Website>> GetUserWebsitesAsync(int userId)
        {
            var websites = new List<Website>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                "SELECT * FROM Websites WHERE UserId = @UserId", conn);
            cmd.Parameters.AddWithValue("UserId", userId);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                websites.Add(new Website
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Domain = reader.GetString(reader.GetOrdinal("Domain")),
                    OwnerId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    PlanId = reader.GetInt32(reader.GetOrdinal("PlanId")),
                    Status = reader.GetString(reader.GetOrdinal("Status")),
                    UsedDiskMB = reader.GetDecimal(reader.GetOrdinal("UsedDiskMB"))
                });
            }
            return websites;
        }
        public async Task CreateRequestAsync(int userId, string requestType, string description)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                @"INSERT INTO Requests (UserId, RequestType, Description, Status) 
          VALUES (@UserId, @RequestType, @Description, 'Pending')", conn);

            cmd.Parameters.AddWithValue("UserId", userId);
            cmd.Parameters.AddWithValue("RequestType", requestType);
            cmd.Parameters.AddWithValue("Description", description);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}