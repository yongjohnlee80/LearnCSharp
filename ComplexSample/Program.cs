using System;
using System.Collections.Generic;
using ComplexLib;
using static ComplexLib.TPerson;
using System.Text.RegularExpressions;

namespace ComplexSample
{
    public static class StringExtensions {
        public static bool IsValidEmail(this string input) {
            return Regex.IsMatch(input, @"[a-zA-Z0-9.-_]+@[a-zA-Z0-9\.-_]+");
        }
    }

    public static class TPersonExtensions {
        public static TPerson SearchName(this List<TPerson> input, string name) {
            foreach(var person in input) {
                if(person.Name == name) return person;
            }
            return null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<TPerson>();
            people.Add(new TPerson("John"));
            people.Add(new TPerson("David"));
            people.Add(new TPerson("Sarah"));
            people.Add(new TPerson("Nina"));
            people.Add(new TEmployee("Cindy",new DateTime(2005, 2, 7), "3825", new DateTime(2021, 10, 3)));
            people.Sort();

            //TPerson john = SearchName(people, "John");
            TPerson john = people.SearchName("John");
            john.ProcreateWith(people.SearchName("Sarah"));
            john.ProcreateWith(people.SearchName("Nina"));
            john.ProcreateWith(people.SearchName("Cindy"));

            Console.WriteLine(john.Name.IsValidEmail());

            foreach(var person in people) {
                Console.WriteLine(person);
                Console.WriteLine($"{person.Name} has {person.GetNChildren()} children as follows: ");
                foreach(var child in person.Children) {
                    Console.WriteLine(child);
                }
                Console.WriteLine();
            }

            string city = "London";
            Console.WriteLine($"{city} is {city.Length} characters long.");
            Console.WriteLine($"First char is {city[0]} and third is {city[2]}");

            string cities = "London, Vancouver, Seattle, Los Angeles, New York";
            cities = cities.Replace(" ", "");
            string[] citiesArray = cities.Split(',');
            foreach(string item in citiesArray) {
                Console.WriteLine(item);
            }

            Console.Write("Enter your age: ");
            string input = Console.ReadLine();
            var ageChecker = new Regex(@"^[0-9]$");
            if (ageChecker.IsMatch(input)) {
                Console.WriteLine("Thank you.");
            } else {
                Console.WriteLine($"This is not a valid age: {input}");
            }

            Console.ReadLine();
        }
    }
}
