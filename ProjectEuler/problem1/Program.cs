// 967324

using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class Problem1
    {
        public static int Solve(int k) {
            int result = 0;
            for(int i=1; i<k; i++) {
                if(i%3==0 || i%5==0) {
                    result+=i;
                }
            }
            return result;
        }

        public static int Solve2(int k) {
            int SumDivisibleBy(int n, int k) {
                int p = k / n;
                return n*(p*(p+1))/2;
            }
            return SumDivisibleBy(3,k)+SumDivisibleBy(5,k)-SumDivisibleBy(15,k);
        }

        public static int SampleRegex() {
            string[] data = new string[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };
            int result = 0;
            foreach (string s in data)
            {
                string[] token = Regex.Split(s, @"\s+");
                string[] numbers = Regex.Split(token[0], @"\D+");
                int min, max;
                int.TryParse(numbers[0], out min);
                int.TryParse(numbers[1], out max);
                foreach(string i in token) {
                    Console.WriteLine(i);
                }
                Console.WriteLine($"min: {min}, max:{max}");

                char[] key = token[1].ToCharArray();
                int f = token[2].Where(x=>x==key[0]).Count();
                if (f>=min && f<=max)
                {
                    result++;
                }
            }
            return result;

        }
        public static void Main(string[] args) {
            Console.WriteLine(Solve2(999));
            Console.WriteLine(SampleRegex());
        }
    }
}