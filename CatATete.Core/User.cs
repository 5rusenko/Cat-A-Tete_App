// User.cs (in CatATete.Core)
using System;
using System.IO;

namespace CatATete.Core
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private static int userCounter;

        static User()
        {
            // Load the user counter from a file or initialize it to 0
            LoadUserCounter();
        }

        // Modify the constructor to accept UserId as a parameter
        public User(string userId, string firstName, string lastName, string username, string password)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;

            // Save the updated user counter after creating a new user
            SaveUserCounter();
        }

        // Additional constructor without UserId parameter for convenience
        public User(string firstName, string lastName, string username, string password)
            : this($"USER_{++userCounter:D4}", firstName, lastName, username, password)
        {
        }

        // Helper method to load the user counter from a file
        private static void LoadUserCounter()
        {
            // Load user counter from a file; initialize to 0 if the file is not found
            string filePath = "userCounter.txt";

            if (File.Exists(filePath))
            {
                string counterString = File.ReadAllText(filePath);
                int.TryParse(counterString, out userCounter);
            }
        }

        // Helper method to save the user counter to a file
        private static void SaveUserCounter()
        {
            // Save the user counter to a file
            string filePath = "userCounter.txt";
            File.WriteAllText(filePath, userCounter.ToString());
        }
    }
}
