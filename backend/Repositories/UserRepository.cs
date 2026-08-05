using backend.Database;
using Microsoft.Data.SqlClient;
using backend.DTOs;
using backend.Models;

namespace backend.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public UserRepository(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public string TestDatabaseConnection()
        {
            using SqlConnection connection = _databaseHelper.GetConnection();

            connection.Open();

            string query = "SELECT GETDATE();";

            using SqlCommand command = new SqlCommand(query, connection);

            object? result = command.ExecuteScalar();

            return result?.ToString() ?? "No Result";
        }

        public bool EmailExists(string email)
        {
            using SqlConnection connection = _databaseHelper.GetConnection();

            connection.Open();

            string query = @"
                SELECT COUNT(*)
                FROM Users
                WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            int count = (int)command.ExecuteScalar()!;

            return count > 0;
        }

        public void CreateUser(RegisterRequest request)
        {
            using SqlConnection connection = _databaseHelper.GetConnection();

            connection.Open();

            string query = @"
                INSERT INTO Users
                (
                    FullName,
                    Email,
                    PasswordHash,
                    CreatedAt
                )
                VALUES
                (
                    @FullName,
                    @Email,
                    @PasswordHash,
                    @CreatedAt
                )";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FullName", request.FullName);
            command.Parameters.AddWithValue("@Email", request.Email);


            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);

            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

            command.ExecuteNonQuery();
        }

        public User? GetUserByEmail(string email)
        {
            using SqlConnection connection = _databaseHelper.GetConnection();

            connection.Open();

            string query = @"
                SELECT
                    Id,
                    FullName,
                    Email,
                    PasswordHash,
                    CreatedAt
                FROM Users
                WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FullName = reader["FullName"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    PasswordHash = reader["PasswordHash"].ToString()!,
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            return null;
        }
    }
}
