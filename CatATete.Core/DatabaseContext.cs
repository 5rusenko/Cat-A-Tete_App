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
                    reader.GetString(0), // UserId
                    reader.GetString(1), // FirstName
                    reader.GetString(2), // LastName
                    reader.GetString(3), // Username
                    reader.GetString(4)  // Password
                )
                {
                    UserId = reader.GetString(0)
                };
            }

            // If no user found, return null
            return null;

            var createCatsTableCommand = connection.CreateCommand();
            createCatsTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Cats (
                    CatId TEXT PRIMARY KEY,
                    Name TEXT,
                    BirthDate TEXT,
                    Breed TEXT,
                    Color TEXT,
                    UserId TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                )";
            createCatsTableCommand.ExecuteNonQuery();
        }

        public static void InsertCat(Cat cat, string userId)
        {
            using var insertConnection = new SqliteConnection(ConnectionString);
            insertConnection.Open();

            var insertCommand = insertConnection.CreateCommand();
            insertCommand.CommandText = "INSERT INTO Cats (CatId, Name, BirthDate, Breed, Color, UserId) VALUES (@CatId, @Name, @BirthDate, @Breed, @Color, @UserId)";
            insertCommand.Parameters.AddWithValue("@CatId", cat.CatId);
            insertCommand.Parameters.AddWithValue("@Name", cat.Name);
            insertCommand.Parameters.AddWithValue("@BirthDate", cat.BirthDate.ToString("yyyy-MM-dd"));
            insertCommand.Parameters.AddWithValue("@Breed", cat.Breed);
            insertCommand.Parameters.AddWithValue("@Color", cat.Color);
            insertCommand.Parameters.AddWithValue("@UserId", userId);

            insertCommand.ExecuteNonQuery();
        }

        public static Cat GetCatByName(string catName)
        {
            if (connection is null)
            {
                Console.WriteLine("Connection is not initialized.");
                return new Cat("", DateTime.Now, "", ""); // Modify with appropriate default values
            }

            using var selectConnection = new SqliteConnection(ConnectionString);
            selectConnection.Open();

            var selectCommand = selectConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM Cats WHERE Name = @Name";
            selectCommand.Parameters.AddWithValue("@Name", catName);

            using var reader = selectCommand.ExecuteReader();

            if (reader.Read())
            {
                return new Cat(
                    reader.GetString(1), // Name
                    DateTime.Parse(reader.GetString(2)), // BirthDate
                    reader.GetString(3), // Breed
                    reader.GetString(4)  // Color
                )
                {
                    CatId = reader.GetString(0)
                };
            }

            return new Cat("", DateTime.Now, "", ""); // Modify with appropriate default values
        
}
    }
}
