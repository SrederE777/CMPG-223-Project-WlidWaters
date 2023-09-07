using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Employee : Person
    {
        // Properties
        public string EmergencyContact { get; set; }
        public string Password { get; set; }
        public string Ride { get; set; }

        public Employee() : this("","", "", DateTime.Now, "", "", "")
        {
            // Initialize properties with default values if desired
        }

        // Constructors
        public Employee(string emergencyContact, string password, string ride) : base()
        {
            EmergencyContact = emergencyContact;
            Password = password;
            Ride = ride;
        }

        // Constructor that takes parameters for base class properties
        public Employee(string name, string surname, string contact, DateTime birthday, string emergencyContact, string password, string ride) : base(name ,surname, contact, birthday)
        {
            EmergencyContact = emergencyContact;
            Password = password;
            Ride = ride;
        }

        public override List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("EmergencyContact");
            names.Add("Password");
            names.Add("Ride");
            names.Add("Name");
            names.Add("Surname");
            names.Add("Contact");
            names.Add("Birthday");
            
            return names;

        }

        public override Dictionary<string, string> getDataBaseName()
        {
            Dictionary<string,string> returnDic = base.getDataBaseName();
            returnDic["Name"] = "Employee_Name";
            returnDic["Surname"] = "Employee_Surname";
            returnDic["birthday"] = "Employee"
        }

        

        // Override ToString() method
        public override string ToString()
        {
            return base.ToString() + $"\nEmergency Contact: {EmergencyContact}\nPassword: {Password}\nRide: {Ride}";
        }

    }


}