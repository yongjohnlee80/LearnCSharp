using System;

namespace switchexp
{
    class Program
    {
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
        }
    }
}
