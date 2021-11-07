using System;
using System.Diagnostics;
using System.IO;

namespace switchexp
{
    class Program
    {
        /// <summary>
        /// computes the taxable portion based on regional tax rate.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="regionalCode">Two letter rigional codes.</param>
        /// <returns></returns>
        static decimal CalculateTax(decimal amount, string regionalCode) {
            var rate = regionalCode switch {
                "CH" => 0.08M,
                "DK" => 0.25M,
                "GB" => 0.2M,
                "HU" => 0.27M,
                "CA" => 0.0825M,
                _ => 0.07M
            };
            return amount * rate;
        }
        static void Main(string[] args)
        {
       
            Console.WriteLine(CalculateTax(135.0M, "CA"));

            double a = 3.5, b = 4.2;
            Console.WriteLine($"{a} + {b} = {add(a,b):N2}");

            AddListener("log.txt");
            LogTrace("Logging Message");
            
        }

        static double add(double a, double b) {
            return a+b;
        }

        static void AddListener(string fileName) {
            Trace.Listeners.Add(new TextWriterTraceListener(File.CreateText(fileName)));
            Trace.AutoFlush = true;
        }

        static void LogTrace(string msg) {
            Debug.WriteLine(msg);
            Trace.WriteLine(msg);
        }
    }
}
