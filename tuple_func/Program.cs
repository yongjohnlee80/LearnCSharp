using System;

namespace tuple_func
{
    class Person
    {
        string name;
        int age;
        public Person(string name = "adam", int age = 20) {
            this.name = name;
            this.age = age;            
        }
        public (string, int) GetFruit() {
            return ("apple", 5);
        }

        public void Add(int x, ref int y, out int z) {
            y += x;
            z = y;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var adam = new Person(age: 35);
            Console.WriteLine(adam.GetFruit());

            int a = 5, b = 6;
            ref int d = ref a;
            adam.Add(a, ref b, out int c);
            Console.WriteLine($"a: {a}, b: {b}, c:{c}");
            Console.WriteLine($"d: {d}");
        }
    }
}
