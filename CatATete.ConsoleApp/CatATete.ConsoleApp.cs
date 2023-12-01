// Program.cs (in CatATete.ConsoleApp)
using System;
using CatATete.Core;

namespace CatATete.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Cat-A-Tete App!");

            while (true)
            {
                if (AuthService.GetLoggedInUser() == null)
                {
                    DisplayLoginOrRegisterMenu();
                }
                else
                {
                    DisplayMainMenu();
                }
            }
        }

        private static void DisplayLoginOrRegisterMenu()
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");

            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static void RegisterUser()
        {
            Console.WriteLine("Enter your details:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            AuthService.RegisterUser(firstName, lastName, username, password);

            Console.WriteLine("Registration successful!");
        }

        private static void Login()
        {
            Console.WriteLine("Enter your login details:");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (AuthService.Login(username, password))
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
            }
        }

        private static void DisplayMainMenu()
        {
            // Add the main menu options here
            Console.WriteLine("Main Menu (Add your options here)");
            Console.WriteLine("1. Option 1");
            Console.WriteLine("2. Option 2");
            Console.WriteLine("3. Logout");

            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    // Implement the functionality for Option 1
                    break;
                case 2:
                    // Implement the functionality for Option 2
                    break;
                case 3:
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static void Logout()
        {
            AuthService.Logout();
            Console.WriteLine("Logout successful!");
        }

        private static int GetIntInput(string message)
        {
            int input;
            Console.Write(message);

            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write(message);
            }

            return input;
        }
    }
}
