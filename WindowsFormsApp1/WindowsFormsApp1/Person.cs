using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    abstract class Person : DataClasses
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
        abstract public List<string> getName();


        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday.ToShortDateString()}";
        }
    }
}

