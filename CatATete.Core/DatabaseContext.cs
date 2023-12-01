// DatabaseContext.cs (in CatATete.Core)
using Microsoft.Data.Sqlite;
using System;

namespace CatATete.Core
{
    public static class DatabaseContext
    {
        private const string ConnectionString = "Data Source=catatete.db";

        static DatabaseContext()
        {
            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            Console.WriteLine("Initializing the database...");
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var createTableCommand = connection.CreateCommand();
            createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId TEXT PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT,
                    Username TEXT,
                    Password TEXT
                )";
            createTableCommand.ExecuteNonQuery();
        }

        public static void InsertUser(User user)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = "INSERT INTO Users (UserId, FirstName, LastName, Username, Password) VALUES (@UserId, @FirstName, @LastName, @Username, @Password)";
            insertCommand.Parameters.AddWithValue("@UserId", user.UserId);
            insertCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", user.LastName);
            insertCommand.Parameters.AddWithValue("@Username", user.Username);
            insertCommand.Parameters.AddWithValue("@Password", user.Password);

            insertCommand.ExecuteNonQuery();
        }
    }
}
