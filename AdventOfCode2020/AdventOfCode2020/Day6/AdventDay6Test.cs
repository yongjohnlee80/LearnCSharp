using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day6Tests
    {
        string[]? data = null;

        [Test]
        public void TestExample()
        {
            var filename = Path.Combine(System.Environment.CurrentDirectory, "Day6Sample.txt");
            this.data = File.ReadAllLines(filename);
            var test = new AdventDay6(this.data);
            Assert.That(test.FindSolution(), Is.EqualTo(11));
        }

        [Test]
        public void TestSolution()
        {
            var filename = Path.Combine(System.Environment.CurrentDirectory, "Day6Data.txt");
            this.data = File.ReadAllLines(filename);
            var test = new AdventDay6(this.data);
            Console.WriteLine(test.FindSolution());
        }
    }
}
