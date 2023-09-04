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
        private string rideDescription;
        private bool rideAvailability;
        private double rideCost;
        private int rideLength;
        private string ridePhotoName;

        public void SetRideDescription(string rideDescription)
        {
            try
            {
                this.rideDescription = rideDescription;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetRideAvailability(bool rideAvailability)
        {
            try
            {
                this.rideAvailability = rideAvailability;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetRideCost(double rideCost)
        {
            try
            {
                this.rideCost = rideCost;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetRideLength(int rideLength)
        {
            try
            {
                this.rideLength = rideLength;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetRidePhotoName(string ridePhotoName)
        {
            try
            {
                this.ridePhotoName = ridePhotoName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
