using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public delegate bool FieldConstraints(string field);
    public class TravelDocument
    {
        // fields
        public Dictionary<string, string> fields = new Dictionary<string, string>();
        FieldConstraints? rule;

        // methods.
        public bool Validate(string[] requiredFields, bool constraints = false, FieldConstraints? customRule = null)
        {
            rule = (customRule == null)? CheckConstraint : customRule;
            foreach (var field in requiredFields)
            {

                if (!fields.ContainsKey(field))
                {
                    return false;
                } 
                else if (constraints)
                {
                    if (!rule(field)) return false;
                }
            }
            return true;
        }

        public bool CheckConstraint(string field)
        {
            try
            {
                switch (field)
                {
                    case "byr":
                        int byr = int.Parse(fields[field]);
                        if(byr >= 1920 && byr <= 2002) return true;
                        break;
                    case "iyr":
                        int iyr = int.Parse(fields[field]);
                        if (iyr >= 2010 && iyr <= 2020) return true;
                        break;
                    case "eyr":
                        int eyr = int.Parse(fields[field]);
                        if (eyr >= 2020 && eyr <= 2030) return true;
                        break;
                    case "hgt":
                        string[] temp = Regex.Split(fields[field], @"\D+");
                        int hgt = int.Parse(temp[0]);
                        if (fields[field].Contains("cm"))
                        {
                            if (hgt >= 150 && hgt <= 193) return true;
                        } else if (fields[field].Contains("in"))
                        {
                            if (hgt >= 59 && hgt <= 76) return true;
                        }
                        break;
                    case "hcl":
                        if (Regex.IsMatch(fields[field], @"^#([a-fA-F0-9]{6})$")) return true;
                        break;
                    case "ecl":
                        HashSet<string> colors = new HashSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                        if(colors.Contains(fields[field])) return true;
                        break;
                    case "pid":
                        if (Regex.IsMatch(fields[field], @"^([0-9]{9})$")) return true;
                        break;
                    default:
                        break;
                }
            } 
            catch
            {
                Console.WriteLine("Incorrect Document Form Fields");
            }
            return false;
        }
    }
    public static class TravelDocumentExtensions
    {
        public static void LoadDocuments(this List<TravelDocument> input, string[] lines)
        {
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line)) input.Add(new TravelDocument());
                else
                {
                    string[] fields = Regex.Split(line, @"\s+");
                    foreach (string field in fields)
                    {
                        string[] parts = field.Split(':');
                        input[input.Count - 1].fields.Add(parts[0], parts[1]);
                    }
                }
            }
        }
        
        public static int CountValidDocuments(this List<TravelDocument> input, string[] requiredFields, bool constraints = false)
        {
            int count = 0;
            foreach (TravelDocument document in input)
            {
                if (document.Validate(requiredFields, constraints))
                {
                    count++;
                }
            }
            return count;
        }
    }
    internal class AdventDay4
    {
        // fields

        string[] data;
        // constructor
        public AdventDay4(string[] data)
        {
            this.data = data;
        }
        // methods.
        public int FindSolution(string[] requiredFields, bool checkConstraints = false)
        {
            List<TravelDocument> documents = new List<TravelDocument>();
            documents.Add(new TravelDocument());
            documents.LoadDocuments(data);
            return documents.CountValidDocuments(requiredFields, checkConstraints);
        }
    }
}
