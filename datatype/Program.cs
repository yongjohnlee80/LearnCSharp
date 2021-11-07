using System;
using System.IO;
using System.Collections.Generic;
using static System.Convert;
using static System.Console;

namespace datatype
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sampleList = null;
            var namesList = new List<string>();

            (sampleList ??= new List<int>()).Add(6);
            sampleList.Add(3);
            WriteLine(string.Join(", ",sampleList));

            namesList.Add("John");
            namesList.Add("Kee");
            namesList.Add("Tiger");
            WriteLine(string.Join(", ",namesList));

            foreach(string n in namesList) {
                WriteLine(n);
            }

            double[] scores = {9.49, 9.5, 9.51, 10.49, 10.5, 10.51};
            foreach(double n in scores) {
                WriteLine($"ToInt({n}) is {ToInt32(n)}");
                WriteLine($"Ronud({n}) is {Math.Round(n,0,MidpointRounding.AwayFromZero)}");
            }
            WriteLine(string.Join(", ", scores));

            var binObj = new byte[128];
            (new Random()).NextBytes(binObj);
            foreach(var i in binObj) {
                Write($"{i:X} ");
            }
            string encoded = Convert.ToBase64String(binObj);
            WriteLine($"\nBinary Object as Base64: {encoded}");
        }
    }
}
