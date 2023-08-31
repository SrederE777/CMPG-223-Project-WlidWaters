using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Employee
    {
        // Properties
        public string EmergencyContact { get; private set; }
        private string Password { get; set; }
        public string Ride { get; private set; }

        // Constructors
        public Employee(string emergencyContact, string password, string ride)
        {
            EmergencyContact = emergencyContact;
            Password = password;
            Ride = ride;
        }

        // Methods to set properties
        public void SetEmergencyContact(string emergencyContact)
        {
            EmergencyContact = emergencyContact;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetRide(string ride)
        {
            Ride = ride;
        }

        // Methods to get properties
        public string GetPassword()
        {
            return Password;
        }

        public string GetRide()
        {
            return Ride;
        }

        public string GetEmergencyContact()
        {
            return EmergencyContact;
        }

        // Method to check if a provided password matches the stored password
        public bool CheckPassword(string password)
        {
            return Password == password;
        }

        // Method to assign an employee to a ride
        public void AssignToRide(string ride)
        {
            Ride = ride;
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Emergency Contact: {EmergencyContact}\nRide: {Ride}";
        }
    }
}
