using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Rides
    {
        private String rideDescription;
        private bool rideAvailability;
        private double rideCost;
        private int rideLength;
        private String ridePhotoName;

        public void setRideDescription(String rideDescription)
        {
            this.rideDescription = rideDescription;
        }

        public void setRideAvailability(bool rideAvailability)
        {
            this.rideAvailability = rideAvailability;
        }

        public void setRideCost(double rideCost)
        {
            this.rideCost = rideCost;
        }

        public void setRideLength(int rideLength)
        {
            this.rideLength = rideLength;
        }

        public void setRidePhotoName(String ridePhotoName)
        {
            this.ridePhotoName = ridePhotoName;
        }

        public String getRideDescription()
        {
            return rideDescription;
        }

        public bool getRideAvailability()
        {
            return rideAvailability;
        }

        public double getRideCost()
        {
            return rideCost;
        }

        public int getRideLength()
        {
            return rideLength;
        }

        public String getRidePhotoName()
        {
            return ridePhotoName;
        }

        public void showPhoto()
        {
            // Implement logic to display the ride photo
        }

        
        public String toString()
        {
            return "Ride Description: " + rideDescription + "\n"
                    + "Ride Availability: " + rideAvailability + "\n"
                    + "Ride Cost: " + rideCost + "\n"
                    + "Ride Length: " + rideLength + "\n"
                    + "Ride Photo Name: " + ridePhotoName;
        }

    }
}
