// Cat.cs (in CatATete.Core)
using System;

namespace CatATete.Core
{
    public class Cat
    {
        public string CatId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }

        private static int catCounter = 0;

        public Cat(string name, DateTime birthDate, string breed, string color)
        {
            catCounter++;
            CatId = $"CAT_{catCounter:D4}";
            Name = name;
            BirthDate = birthDate;
            Breed = breed;
            Color = color;
        }

        public string GetCurrentAge()
        {
            // Implement logic to calculate and return current age in the format "XX years XX months"
            TimeSpan age = CalculateAge(BirthDate, DateTime.Now);
            return $"{age.Days / 365} years {age.Days % 365 / 30} months";
        }

        private static TimeSpan CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            if (birthDate > currentDate)
            {
                throw new ArgumentException("Birth date cannot be in the future.");
            }

            int years = currentDate.Year - birthDate.Year;
            int months = currentDate.Month - birthDate.Month;

            if (currentDate.Day < birthDate.Day)
            {
                months--;
            }

            // Adjust for cases where birth date day is greater than current date day
            if (months < 0)
            {
                years--;
                months += 12;
            }

            return new TimeSpan(years, months, 0, 0, 0);
        }
    }
}
