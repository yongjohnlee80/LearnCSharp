using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    /*************************************************************************
     * class for unit testing.
     * delete it before integration
     **************************************************************************/
    internal class AdventDay7
    {
        // fields
        string[] data;
        // constructor
        public AdventDay7(string[] data)
        {
            this.data = data;
        }
        // methods.
        public int FindSolution(string name)
        {
            var rules = new BagRules();
            rules.LoadRules(data);
            return (rules.FindRulesWith(name)).Count;
        }

        public int FindSolution2(string name)
        {
            var rules = new BagRules();
            rules.LoadRules(data);
            return rules.GetNumberOfBags(name);
        }
    }

    /**************************************************************************
     * Implement classes on separate files once test is completed.
     * Author: Yong Sung Lee.
     * Date: 2021-11-25.
     **************************************************************************/


    /**************************************************************************
     * Type Bag
     **************************************************************************/
    public class Bag
    {
        // fields
        public Dictionary<string, int> children = new Dictionary<string, int>();
        public Bag(string name)
        {
            Name = name;
        }

        public void AddChild(string name, int count)
        {
            children.Add(name, count);
        }

        public bool HasChild(string name) { return children.ContainsKey(name); }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    // Extention Methods.
    public static class BagExtention
    {
        public static Bag? FindName(this List<Bag> input, string name)
        {
            foreach(Bag i in input)
            {
                if(i.Name == name) return i;
            }
            return null;
        }
        public static List<Bag> FindParentsWith(this List<Bag> input, Bag child)
        {
            var result = new List<Bag>();
            foreach(Bag i in input)
            {
                if(i.HasChild(child.Name))
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }

    /**************************************************************************
    * Type BagRules
    **************************************************************************/
    public class BagRules
    {
        // fields
        public List<Bag> rule = new List<Bag>();
        public int Count
        {
            get { return rule.Count; }
        }

        public void LoadRules(string[] lines)
        {
            StringBuilder log = new StringBuilder(); // debug
            foreach (string line in lines)
            {
                string[] parts = line.Split("contain");
                string nodeName = parts[0].Replace("bags", "").Replace("bag", "").Trim();
                if(rule.FindName(nodeName)==null)
                {
                    rule.Add(new Bag(nodeName));
                    log.Append($"\n{nodeName}\n"); // debug
                    string[] nodes = parts[1].Split(",");
                    foreach (string node in nodes)
                    {
                        if (node.Contains("no other bags")) break;
                        string temp = node.Replace(".", "").Trim();
                        string[] childName = Regex.Split(temp, @"\d+");
                        string[] quantity = Regex.Split(temp, @"\D+");
                        string name = childName[1].Replace("bags", "").Replace("bag", "").Trim();
                        int number = Convert.ToInt32(quantity[0]);
                        rule[rule.Count - 1].AddChild(name, number);
                        log.Append($"[{name} : {number}]\n"); // debug
                    }
                    log.Append("\n"); // debug
                }
            }
            File.WriteAllText("log.txt", log.ToString()); // debug  
        }

        public List<Bag> FindRulesWith(string name)
        {
            var result = new HashSet<Bag>();
            var queue = new HashSet<Bag>();
            var start = rule.FindName(name);
            if(start != null)
            {
                queue.Add(start);
                while(queue.Count > 0)
                {
                    Bag i = queue.ElementAt(0);
                    queue.Remove(i);
                    List<Bag> found = rule.FindParentsWith(i);
                    if(found.Count > 0)
                    {
                        foreach(var item in found)
                        {
                            result.Add(item);
                            queue.Add(item);
                        }
                    }
                    queue.Remove(i);
                }
            }
            return result.ToList();
        }

        public int GetNumberOfBags(string name)
        {
            var bag = rule.FindName(name);
            if (bag.children.Count == 0)
            {
                return 1;
            }
            else
            {
                int count = 1;
                foreach(var i in bag.children)
                {
                    count += (i.Value * GetNumberOfBags(i.Key));
                }
                return count;
            }
        }

        //public int HowManyBags(string name)
        //{
        //    int count = 0;
        //    var bags = new HashSet<string>();
        //    BuildQueue(name);
        //    foreach(var i in bags)
        //    {
        //        var item = rule.FindName(i);
        //        if(item != null)
        //        {
        //            foreach (var j in item.children)
        //            {
        //                count += j.Value;
        //            }
        //        }
        //    }
        //    return count;
        //}
    }
}
