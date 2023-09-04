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
            this.rideDescription = rideDescription;
        }

        // Constructor that takes parameters for all properties
        public Rides(string rideDescription, bool rideAvailability, double rideCost, int rideLength, string ridePhotoName)
        {
            this.rideAvailability = rideAvailability;
        }

        public void ShowPhoto()
        {
            this.rideCost = rideCost;
        }

        public override string ToString()
        {
            this.rideLength = rideLength;
        }

        public void SetRidePhotoName(string ridePhotoName)
        {
            this.ridePhotoName = ridePhotoName;
        }


}

