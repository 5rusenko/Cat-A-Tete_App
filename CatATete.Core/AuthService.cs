// AuthService.cs (in CatATete.Core)
using System;

namespace CatATete.Core
{
    public static class AuthService
    {
        static AuthService()
        {
            DatabaseContext.InitializeDatabase();
        }

        private static User? loggedInUser;

        public static void RegisterUser(string firstName, string lastName, string username, string password)
        {
            if (DatabaseContext.GetUserByUsername(username) != null)
            {
                Console.WriteLine("User with the same username already exists. Please choose a different username.");
                return;
            }

            var newUser = new User(firstName, lastName, username, password);
            DatabaseContext.InsertUser(newUser);
        }

        public static bool Login(string username, string password)
        {
            var user = DatabaseContext.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                loggedInUser = user;
                return true;
            }

            return false;
        }

        public static User? GetLoggedInUser()
        {
            return loggedInUser;
        }

        public static void Logout()
        {
            loggedInUser = null;
        }
    }
}
