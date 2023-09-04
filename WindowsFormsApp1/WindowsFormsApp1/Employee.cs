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

        public Employee() : this("", "", DateTime.Now, "", "", "")
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
        public Employee(string name, string contact, DateTime birthday, string emergencyContact, string password, string ride) : base(name, contact, birthday)
        {
            EmergencyContact = emergencyContact;
            Password = password;
            Ride = ride;
        }

        // Override ToString() method
        public override string ToString()
        {
            return base.ToString() + $"\nEmergency Contact: {EmergencyContact}\nPassword: {Password}\nRide: {Ride}";
        }

    }


}