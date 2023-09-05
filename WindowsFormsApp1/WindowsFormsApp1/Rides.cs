using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Rides : DataClasses
    {
        public string Ride_Name { get; set; }
        public string Ride_Description { get; set; }
        public bool Ride_Availability { get; set; }
        public double Ride_Cost { get; set; }
        public int Ride_Length { get; set; }
        

        // Default constructor
        public Rides()
        {
            // Initialize properties with default values if desired
        }

        // Constructor that takes parameters for all properties
        public Rides(string rideName, string rideDescription, bool rideAvailability, double rideCost, int rideLength)
        {
            Ride_Name = rideName;
            Ride_Description = rideDescription;
            Ride_Availability = rideAvailability;
            Ride_Cost = rideCost;
            Ride_Length = rideLength;
        }

        public List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("Ride Name");
            names.Add("Ride Description");
            names.Add("Ride Availability");
            names.Add("Ride Cost");
            names.Add("Ride Length");
            return names;

        }

        public override string ToString()
        {
            return $"Ride Name: {Ride_Name}\n" +
                   $"Ride Description: {Ride_Description}\n" +
                   $"Ride Availability: {Ride_Availability}\n" +
                   $"Ride Cost: {Ride_Cost}\n" +
                   $"Ride Length: {Ride_Length}\n";
        }
    }

}

