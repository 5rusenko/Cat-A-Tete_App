// DatabaseContext.cs (in CatATete.Core)
using Microsoft.Data.Sqlite;
using System;

namespace CatATete.Core
{
    public static class DatabaseContext
    {
        private const string ConnectionString = "Data Source=catatete.db";
        private static SqliteConnection? connection = null;

        static DatabaseContext()
        {
            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            Console.WriteLine("Initializing the database...");
            connection = new SqliteConnection(ConnectionString);
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


        public static User GetUserByUsername(string username)
        {
            if (connection is null)
            {
                // Handle the case when the connection is null
                Console.WriteLine("Connection is not initialized.");
                // Return a default user or throw an exception, based on your requirements
                return new User("", "", "", "");
            }

            using var selectConnection = new SqliteConnection(ConnectionString);
            selectConnection.Open();

            var selectCommand = selectConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM Users WHERE Username = @Username";
            selectCommand.Parameters.AddWithValue("@Username", username);

            using var reader = selectCommand.ExecuteReader();

            if (reader.Read())
            {
                return new User(
                    reader.GetString(1), // FirstName
                    reader.GetString(2), // LastName
                    reader.GetString(3), // Username
                    reader.GetString(4)  // Password
                )
                {
                    UserId = reader.GetString(0)
                };
            }

            // If no user found, return a default User instance or throw an exception
            return new User("", "", "", ""); // Modify with appropriate default values
        }
    }
}
