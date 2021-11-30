using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    /// <summary>
    /// Type AdventDay8
    /// This is the unit testing entry.
    /// Various Solution methods are implmented to interact with the unit to be tested.
    /// This class also modifies the training data into proper data structure.
    /// </summary>
    internal class AdventDay9
    {
        // fields
        string[] data;
        // constructor
        public AdventDay9(string[] data)
        {
            this.data = data;
        }
        // methods.
        public List<long> ConvertData()
        {
            var numbers = new List<long>();
            foreach(string i in this.data)
            {
                numbers.Add(Convert.ToInt64(i));
            }
            return numbers;
        }
        public long FindSolution(int n = 5)
        {
            var numbers = ConvertData();
            PreambleNumber pNumber = new PreambleNumber(numbers, n);

            // For Debug Purpose. Disregard the following code.
            //StringBuilder log = new StringBuilder();
            //foreach (long i in numbers)
            //{
            //    log.Append($"{i}\n");
            //}
            //File.WriteAllText("log.txt", log.ToString());

            return pNumber.FindFirstError();
        }

        public long FindSolution2(int n = 5)
        {
            var numbers = ConvertData();
            PreambleNumber pNumber = new PreambleNumber(numbers, n);
            return pNumber.FindWeakness();
        }
    }

    /**************************************************************************
     * Implement classes on separate files once test is successfully tested.
     * Author: Yong Sung Lee.
     * Date: 2021-11-28.
     **************************************************************************/

    public class PreambleNumber
    {
        List<long> numbers = new List<long>();
        int n;

        public PreambleNumber(List<long> data, int n = 5)
        {
            numbers = data;
            this.n = n;
        }

        public (int, int) FindPreamblePair(int index)
        {
            if (index < n) return (0, 0);
            for(int i = index-n; i <= index-2; i++)
            {
                for(int j = i+1; j <= index-1; j++)
                {
                    if(numbers[i]+numbers[j] == numbers[index]) return (i, j);
                }
            }
            return (0, 0);
        }

        public long FindFirstError()
        {
            int a, b;
            for(int i = n; i < numbers.Count; i++)
            {
                (a, b) = FindPreamblePair(i);
                if (a + b == 0) return numbers[i];
            }
            return -1;
        }

        public long FindWeakness()
        {
            int a, b;
            for (int i = n; i < numbers.Count; i++)
            {
                (a, b) = FindPreamblePair(i);
                if (a + b == 0)
                {
                    List<long>? result = FindContiguous(i);
                    if(result != null)
                    {
                        result.Sort();
                        return result.Last()+result.First();
                    }
                }
            }
            return -1;
        }

        public List<long>? FindContiguous(int index)
        {
            for(int i = 0; i < index; i++)
            {
                long sum = 0;
                int j = i + 1;
                var set = new List<long>();

                while (sum <= numbers[index])
                {
                    set.Add(numbers[j]);
                    sum += numbers[j++];
                    if (sum == numbers[index]) return set;
                }
            }
            return null;
        }
    }
}