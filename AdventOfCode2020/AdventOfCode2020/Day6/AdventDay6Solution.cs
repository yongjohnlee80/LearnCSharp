using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    /*************************************************************************
     * class for unit testing.
     * delete it before integration
     **************************************************************************/
    internal class AdventDay6
    {
        // fields
        string[] data;
        // constructor
        public AdventDay6(string[] data)
        {
            this.data = data;
        }
        // methods.
        public int FindSolution()
        {
            var groups = new List<PassengerGroup>();
            groups.LoadAnswers(data);
            return groups.CountAllYeses();
        }
    }

    /**************************************************************************
     * Implement classes on separate files once test is completed.
     * Author: Yong Sung Lee.
     * Date: 2021-11-24.
     **************************************************************************/


    /**************************************************************************
     * Type PassengerGroup
     **************************************************************************/
    public class PassengerGroup
    {
        // fields
        public HashSet<int> answeredYes = new HashSet<int>();
        public int Count { get; set; } // number of people int the group.
        public int YesAnswered
        {
            get { return answeredYes.Count; }
        }
    }

    /**************************************************************************
     * Extension Methods for List<TravelDocument>
     **************************************************************************/
    public static class PassengerGroupExtensions
    {
        // Load yes answered questions. Each group is separated by a empty new line.
        public static void LoadAnswers(this List<PassengerGroup> input, string[] lines)
        {
            if (input.Count == 0) input.Add(new PassengerGroup()); // add a group if none.
            // Process all lines. Each line represents one person's answers.
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line)) input.Add(new PassengerGroup()); // new group.
                else
                {
                    input[input.Count - 1].Count++;
                    char[] yes = line.ToCharArray();
                    foreach (char i in yes)
                    {
                        input[input.Count - 1].answeredYes.Add(i);
                    }
                }
            }
        }
        // Count the number of yes answered questions from all groups.
        public static int CountAllYeses(this List<PassengerGroup> input)
        {
            int count = 0;
            foreach (PassengerGroup i in input)
            {
                count += i.YesAnswered;
            }
            return count;
        }
    }
}
