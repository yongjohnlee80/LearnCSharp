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
            john.ProcreateWith(SearchName(people, "Sarah"));
            john.ProcreateWith(SearchName(people, "Nina"));
            john.ProcreateWith(SearchName(people,"Cindy"));

            Console.WriteLine(john.Name.IsValidEmail());

            foreach(var person in people) {
                Console.WriteLine(person);
                Console.WriteLine($"{person.Name} has {person.GetNChildren()} children as follows: ");
                foreach(var child in person.Children) {
                    Console.WriteLine(child);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
