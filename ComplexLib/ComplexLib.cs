using System;
using System.Collections.Generic;

namespace ComplexLib
{
    public class TPerson : IComparable<TPerson> {
        // fields.
        protected string _name;
        protected DateTime _DOB;
        protected List<TPerson> _children = new List<TPerson>();

        // Constructor.
        public TPerson(string name = "John", DateTime time = default(DateTime)) {
            Name = name;
            _DOB = time;
        }

        // getters and setters.
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        public DateTime DateOfBirth {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public List<TPerson> Children {
            get { return _children; }
        }
        public int GetNChildren() {
            return Children.Count;
        }

        // indexer.
        public TPerson this[int index] {
            get { return _children[index]; }
            set { _children[index] = value; }
        }

        // methods.
        public virtual void WriteToConsole() {
            Console.WriteLine($"{Name} was born on a {DateOfBirth:yyyy-MM-dd-dddd}.");
        }
        public override string ToString()
        {
            return $"{Name} was born on a {DateOfBirth:yyyy-MM-dd-dddd}.";
        }
        public TPerson ProcreateWith(TPerson partner) {
            return Procreate(this, partner);
        }
        public static TPerson Procreate(TPerson p1, TPerson p2) {
            var baby = new TPerson{
                Name = $"Baby of {p1.Name} and {p2.Name}"
            };
            p1.Children.Add(baby);
            p2.Children.Add(baby);
            return baby;
        }
        public int CompareTo(TPerson other)
        {
            return Name.CompareTo(other.Name);
        }
        public static TPerson SearchName(List<TPerson> list, string name) {
            foreach(var person in list) {
                if(person.Name == name) {
                    return person;
                }
            }
            return null;
        }
    }

    public class TEmployee : TPerson {
        // fields.
        protected string _employeeID;
        protected DateTime _hire_date;

        // getters and setters.
        public string EmployeeID {
            get { return _employeeID; }
            set { _employeeID = value; }
        }
        public DateTime HireDate {
            get { return _hire_date; }
            set { _hire_date = value; }
        }

        // methods.
        public override void WriteToConsole()
        {
            Console.WriteLine($"{Name} was hired on {HireDate:yyyy-MM-dd}");
        }
        public override string ToString()
        {
            return $"{Name}:{EmployeeID} was hired on {HireDate:yyyy-MM-dd}";
        }
    }
}
