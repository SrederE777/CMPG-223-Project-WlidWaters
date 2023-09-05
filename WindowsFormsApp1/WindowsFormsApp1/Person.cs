using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Person : DataClasses
    {
        // Properties
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateTime Birthday { get; set; }

        public Person() : this("", "", DateTime.Now)
        {

        }
        // Constructors
        public Person(string name, string contact, DateTime birthday)
        {
            Name = name;
            Contact = contact;
            Birthday = birthday;
        }

        // Methods to set properties
        public List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("Name");
            names.Add("Contact");
            names.Add("Birthday");
            return names;

        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday.ToShortDateString()}";
        }
    }
}

