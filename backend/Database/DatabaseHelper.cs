using Microsoft.Data.SqlClient;

namespace backend.Database
{
    public class DatabaseHelper
    {
        private readonly IConfiguration _configuration;

        public DatabaseHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string? connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }
    }
}
