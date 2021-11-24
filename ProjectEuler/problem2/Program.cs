using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectEuler
{
    /// <summary>
    /// Type: Fibonacci
    /// Contains hashtable for fast fibonacci number generation.
    /// Uses Recursive Method.
    /// </summary>
    class Fibonacci
    {
        // Fields.
        static Dictionary<int, long> fibTable = new Dictionary<int, long>();  // Hashtable for known Fibonacci terms.

        // Constructor.
        public Fibonacci() {
            // Setup Base Cases if this is the first instance.
            if(!fibTable.ContainsKey(1)) {
                fibTable.Add(1,1L);
                fibTable.Add(2,2L);
            }
        }

        // Methods.
        /// <summary>
        /// Generate Method computes the Fibonacci term for n
        /// Hashtable is used to avoid unnecessary computations.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public long Generate(int n) {
            // check if the term is known.
            if(!fibTable.ContainsKey(n)) {
                fibTable.Add(n, Generate(n-1)+Generate(n-2)); // Generate if unknown.
            }
            return fibTable[n]; // simply return the known term.
        }

        /// <summary>
        /// SumEvenFib method adds all even known terms less than limit.
        /// EulerProject Problem 2 Solution.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public long SumEvenFib(long limit = 4_000_000) {
            long sum = 0;
            int k = 10;
            while(Generate(k)<limit) k++;
            foreach(int i in fibTable.Keys) {
                if(fibTable[i]%2==0 && fibTable[i]<limit) sum+=fibTable[i];
            }
            return sum;
        }
    }
    class Problem2
    {
        public static void Main(string[] arg) {
            var fib = new Fibonacci();
            Console.WriteLine(fib.SumEvenFib(4_000_000));
            Console.WriteLine(Regex.IsMatch("#15E234", @"^#([a-fA-F0-9]{6})$"));
            Console.WriteLine(Regex.IsMatch("10A125678", @"^([0-9]{9})$"));
        }
    }
}