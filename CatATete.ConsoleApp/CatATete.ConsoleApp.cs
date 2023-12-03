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
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Add a Cat");
                Console.WriteLine("2. View all cats");
                Console.WriteLine("3. Logout");

                int choice = GetIntInput("Enter your choice: ");

                switch (choice)
                {
                    case 1:
                        AddCat();
                        break;
                    case 2:
                        ViewAllCats();
                        break;
                    case 3:
                        Logout();
                        return; // Exit the loop after logging out
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void AddCat()
        {
            Console.WriteLine("Enter cat details:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Birth Date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.Write("Breed: ");
                string breed = Console.ReadLine();

                Console.Write("Color: ");
                string color = Console.ReadLine();

                Cat newCat = new Cat(name, birthDate, breed, color);

                // Add the cat to the database
                DatabaseContext.InsertCat(newCat, AuthService.GetLoggedInUser()?.UserId);

                Console.WriteLine("Cat added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
            }
        }

        private static void ViewAllCats()
        {
            // Retrieve cats associated with the logged-in user
            var userId = AuthService.GetLoggedInUser()?.UserId;
            var cats = DatabaseContext.GetCatsByUserId(userId);

            Console.WriteLine("Your Cats:");

            foreach (var cat in cats)
            {
                Console.WriteLine($"Name: {cat.Name}, Birth Date: {cat.BirthDate.ToString("yyyy-MM-dd")}, Breed: {cat.Breed}, Color: {cat.Color}");
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
