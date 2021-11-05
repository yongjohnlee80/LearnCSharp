using System;
using System.IO;
using System.Collections.Generic;

namespace datatype
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sampleList = null;

            (sampleList ??= new List<int>()).Add(6);
            sampleList.Add(3);
            Console.WriteLine(string.Join(",",sampleList));

        }
    }
}
