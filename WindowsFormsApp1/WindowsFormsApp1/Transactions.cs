using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    internal class Transactions : DataClasses
    {
        public foreignKey Customer_ID { get; set; }
        public foreignKey Ride_ID { get; set; }
        public DateTime Transaction_Date { get; set; }
        public TimeSpan Transaction_Time { get; set; }
        private int ticketAmount { get; set; }
        public double Transaction_Amount { get; set; }

        // Default constructor
        public Transactions()
        {
            // Initialize properties with default values if desired
        }

        // Constructor that takes parameters for all properties
        public Transactions(foreignKey customer, foreignKey ride, int ticketAmount)
        {
            Customer_ID = customer;
            Ride_ID = ride;
            Transaction_Date = DateTime.Now; // Current date and time
            Transaction_Time = DateTime.Now.TimeOfDay;
            this.ticketAmount = ticketAmount;
            Transaction_Amount = CalCost();
        }

        public List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("Customer");
            names.Add("Ride");
            names.Add("Transaction Date");
            names.Add("Transaction Time");
            names.Add("Ticket Amount");
            names.Add("Transaction Amount");
            return names;
        }

        public List<string> getDataBaseName()
        {
            List<string> colNames = new List<string>();
            colNames.Add("Customer_ID");
            colNames.Add("Ride_ID");
            colNames.Add("Transaction_Date");
            colNames.Add("Transaction_Time");
            colNames.Add("Ticket_Amount");
            colNames.Add("Transaction_Amount");
            return colNames;
        }

        public double CalCost()
        {
            string sql = "SELECT Ride_Cost FROM Rides WHERE Ride_ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", Ride_ID.value) };
            double cost = 0;
            string costCheck = DataBaseFuncitons.queryDataBase(sql, parameters);
            if (costCheck != "")
                cost = Convert.ToDouble(costCheck);
            else
            {
                throw new Exception("No Ride Selected");
            }
            return cost * ticketAmount;
        }

        public override string ToString()
        {
            return $"Customer: {Customer_ID.name}\n" +
                   $"Customer: {Ride_ID.name}\n" +
                   $"Transaction Date: {Transaction_Date.ToString("yyyy-MM-dd")}\n" +
                   $"Transaction Time: {Transaction_Time.ToString()}\n" +
                   $"Ticket Amount: {ticketAmount}\n" +
                   $"Transaction Amount: {Transaction_Amount}";
        }
    }

}

