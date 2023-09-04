using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Transactions
    {
        public string Customer { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionTime { get; set; }
        public int TicketAmount { get; set; }
        public double TransactionAmount { get; set; }

        // Default constructor
        public Transactions()
        {
            // Initialize properties with default values if desired
        }

        // Constructor that takes parameters for all properties
        public Transactions(string customer, DateTime transactionDate, string transactionTime, int ticketAmount, double transactionAmount)
        {
            Customer = customer;
            TransactionDate = transactionDate;
            TransactionTime = transactionTime;
            TicketAmount = ticketAmount;
            TransactionAmount = transactionAmount;
        }

        public void SellTicket()
        {
            // Implement ticket selling logic here
        }

        public double CalCost()
        {
            // Implement cost calculation logic here
            return 0.0;
        }

        public override string ToString()
        {
            return $"Customer: {Customer}\n" +
                   $"Transaction Date: {TransactionDate.ToString("yyyy-MM-dd")}\n" +
                   $"Transaction Time: {TransactionTime}\n" +
                   $"Ticket Amount: {TicketAmount}\n" +
                   $"Transaction Amount: {TransactionAmount}";
        }
    }

}

