using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Person
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
        public void SetName(string name)
        {
            try
            {
                Name = name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetContact(string contact)
        {
            try
            {
                Contact = contact;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetBirthday(DateTime birthday)
        {
            try
            {
                Birthday = birthday;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Methods to get properties
        public string GetName()
        {
            return Name;
        }

        public string GetContact()
        {
            return Contact;
        }

        public DateTime GetBirthday()
        {
            return Birthday;
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday.ToShortDateString()}";
        }
    }
}

