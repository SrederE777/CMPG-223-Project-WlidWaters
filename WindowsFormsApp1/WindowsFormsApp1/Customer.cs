using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // Define a class that inherits from the Person class

    class Customer : DataClasses
    {
        // Use the auto-implemented property syntax for the email property
        public string Customer_Name { get; set; }
        public string Customer_Surname { get; set; }
        public string Customer_Contact { get; set; }
        public DateTime Customer_DOB  { get; set; }
        public string Customer_Email { get; set; }

        public Customer()
        {
            // Initialization code here
        }

        public Customer(string email)
        {
            Customer_Email = email;
        }

        public Customer(string name, string surname, string contact, DateTime birthday, string email)
        {
            Customer_Name = name;
            Customer_Surname = surname;
            Customer_Contact = contact;
            Customer_DOB = birthday;
            Customer_Email = email;
        }

        public List<string> getName()
        {
            List<string> names = new List<string>();
  
            names.Add("Name");
            names.Add("Surname");
            names.Add("Contact");
            names.Add("Birthday");
            names.Add("Email");
            return names;

        }
        public List<string> getDataBaseName()
        {
            List<string> colNames = new List<string>();
            
            colNames.Add("Customer_Name");
            colNames.Add("Cusomer_Surname");
            colNames.Add("Customer_Contact");
            colNames.Add("Customer_Birthday");
            colNames.Add("Customer_Email");
            return colNames;
        }

        // Override the ToString() method using the string interpolation syntax
        public override string ToString()
        {
            return $"Name: {Customer_Name}\nSurname: {Customer_Surname}\nContact: {Customer_Contact}\nBirthday: {Customer_DOB:d}\nEmail: {Customer_Email}";
        }
    }

}
 