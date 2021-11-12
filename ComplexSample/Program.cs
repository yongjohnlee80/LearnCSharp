using System;
using System.Collections.Generic;
using ComplexLib;
using static ComplexLib.TPerson;

namespace ComplexSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<TPerson>();
            people.Add(new TPerson("John"));
            people.Add(new TPerson("David"));
            people.Add(new TPerson("Sarah"));
            people.Add(new TPerson("Nina"));
            people.Sort();

            TPerson john = SearchName(people, "John");
            john.ProcreateWith(SearchName(people, "Sarah"));
            john.ProcreateWith(SearchName(people, "Nina"));

            foreach(var person in people) {
                Console.WriteLine(person);
                Console.WriteLine($"{person.Name} has {person.GetNChildren()} children as follows: ");
                foreach(var child in person.Children) {
                    Console.WriteLine(child);
                }
                Console.WriteLine();
            }
        }
    }
}
