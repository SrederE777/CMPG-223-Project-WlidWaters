using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Constructors
        public Employee(string emergencyContact, string password, string ride) : base()
        {
            try
            {
                EmergencyContact = emergencyContact;
                Password = password;
                Ride = ride;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Methods to set properties
        public void SetEmergencyContact(string emergencyContact)
        {
            try
            {
                EmergencyContact = emergencyContact;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetPassword(string password)
        {
            try
            {
                Password = password;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetRide(string ride)
        {
            try
            {
                Ride = ride;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
