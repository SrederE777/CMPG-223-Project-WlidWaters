using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Define a constructor that takes an email parameters
        public Customer(string email) : base()
        {
            try
            {
                // Assign the email parameter to the email property
                Email = email;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Define a constructor that takes name, contact, birthday, and email parameters
        public Customer(string name, string contact, DateTime birthday, string email) : base(name, contact, birthday)
        {
            try
            {
                // Assign the email parameter to the email property
                Email = email;
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
        }

        public override List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("Name");
            names.Add("Contact");
            names.Add("Birthday");
            names.Add("Email");
            return names;

        }

        // Override the ToString() method using the string interpolation syntax
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday:d}\nEmail: {Email}";
        }
    }

}
 