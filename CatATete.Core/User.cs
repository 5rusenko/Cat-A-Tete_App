// User.cs (in CatATete.Core)
using System;

namespace CatATete.Core
{
    public class User
    {
        public string UserId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private static int userCounter = 0;

        public User(string firstName, string lastName, string username, string password)
        {
            userCounter++;
            UserId = $"USER_{userCounter:D4}";
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
        }
    }
}
