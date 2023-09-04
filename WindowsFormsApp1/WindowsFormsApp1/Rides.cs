using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Rides
    {
        public string RideDescription { get; set; }
        public bool RideAvailability { get; set; }
        public double RideCost { get; set; }
        public int RideLength { get; set; }
        public string RidePhotoName { get; set; }

        // Default constructor
        public Rides()
        {
            // Initialize properties with default values if desired
        }

        // Constructor that takes parameters for all properties
        public Rides(string rideDescription, bool rideAvailability, double rideCost, int rideLength, string ridePhotoName)
        {
            RideDescription = rideDescription;
            RideAvailability = rideAvailability;
            RideCost = rideCost;
            RideLength = rideLength;
            RidePhotoName = ridePhotoName;
        }

        public void ShowPhoto()
        {
            // Implement logic to display the ride photo
        }

        public override string ToString()
        {
            return $"Ride Description: {RideDescription}\n" +
                   $"Ride Availability: {RideAvailability}\n" +
                   $"Ride Cost: {RideCost}\n" +
                   $"Ride Length: {RideLength}\n" +
                   $"Ride Photo Name: {RidePhotoName}";
        }
    }
}

