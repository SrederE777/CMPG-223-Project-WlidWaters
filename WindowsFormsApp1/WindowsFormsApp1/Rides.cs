using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Rides
    {
        private string rideDescription;
        private bool rideAvailability;
        private double rideCost;
        private int rideLength;
        private string ridePhotoName;

        public void SetRideDescription(string rideDescription)
        {
            this.rideDescription = rideDescription;
        }

        public void SetRideAvailability(bool rideAvailability)
        {
            this.rideAvailability = rideAvailability;
        }

        public void SetRideCost(double rideCost)
        {
            this.rideCost = rideCost;
        }

        public void SetRideLength(int rideLength)
        {
            this.rideLength = rideLength;
        }

        public void SetRidePhotoName(string ridePhotoName)
        {
            this.ridePhotoName = ridePhotoName;
        }

        public string GetRideDescription()
        {
            return rideDescription;
        }

        public bool GetRideAvailability()
        {
            return rideAvailability;
        }

        public double GetRideCost()
        {
            return rideCost;
        }

        public int GetRideLength()
        {
            return rideLength;
        }

        public string GetRidePhotoName()
        {
            return ridePhotoName;
        }

        public void showPhoto()
        {
            // Implement logic to display the ride photo
        }

        
        public string toString()
        {
            return "Ride Description: " + rideDescription + "\n"
                    + "Ride Availability: " + rideAvailability + "\n"
                    + "Ride Cost: " + rideCost + "\n"
                    + "Ride Length: " + rideLength + "\n"
                    + "Ride Photo Name: " + ridePhotoName;
        }

    }
}
