// 967324

using System;

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
        public static void Main(string[] args) {
            Console.WriteLine(Solve2(999));
        }
    }
}