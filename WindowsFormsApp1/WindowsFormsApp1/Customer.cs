using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    // Define a class that inherits from the Person class
    internal class Customer : Person
    {
        // Use the auto-implemented property syntax for the email property
        public string Email { get; set; }

        // Define a public parameterless constructor
        public Customer() : base()
        {
            // Do some initialization here
        }

        // Define a constructor that takes an email parameter
        public Customer(string email) : base()
        {
            // Assign the email parameter to the email property
            Email = email;
        }

        // Define a constructor that takes name, contact, birthday, and email parameters
        public Customer(string name, string contact, DateTime birthday, string email) : base(name, contact, birthday)
        {
            // Assign the email parameter to the email property
            Email = email;
        }

        // Override the ToString() method using the string interpolation syntax
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday:d}\nEmail: {Email}";
        }
    }

}
 