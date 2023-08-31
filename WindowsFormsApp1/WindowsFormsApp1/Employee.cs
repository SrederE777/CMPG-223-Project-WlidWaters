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
        private string EmergencyContact;
        private string Password;
        private string Ride;
        public Employee() : this("","","")
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
        
        // Override ToString() method
        public override string ToString()
        {
            return $"Emergency Contact: {EmergencyContact}\nRide: {Ride}";
        }

    }
}
