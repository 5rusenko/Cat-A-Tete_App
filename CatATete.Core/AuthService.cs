// AuthService.cs (in CatATete.Core)
using System;
using System.Collections.Generic;

namespace CatATete.Core
{
    public static class AuthService
    {
        private static readonly List<User> RegisteredUsers = new List<User>();
        private static User loggedInUser;

        public static void RegisterUser(string firstName, string lastName, string username, string password)
        {
            var newUser = CreateUser(firstName, lastName, username, password);
            RegisteredUsers.Add(newUser);
        }

        public static bool Login(string username, string password)
        {
            foreach (var user in RegisteredUsers)
            {
                if (user.Username == username && user.Password == password)
                {
                    loggedInUser = user;
                    return true;
                }
            }
            return false;
        }

        public static User GetLoggedInUser()
        {
            return loggedInUser;
        }

        public static void Logout()
        {
            loggedInUser = null;
        }

        private static User CreateUser(string firstName, string lastName, string username, string password)
        {
            int userCounter = RegisteredUsers.Count + 1;
            return new User(firstName, lastName, username, password);
        }
    }
}
