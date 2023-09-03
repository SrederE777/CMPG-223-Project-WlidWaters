using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Transactions
    {
        private string customer;
        private DateTime transactionDate;
        private string transactionTime;
        private int ticketAmount;
        private double transactionAmount;

        public void SetCustomer(string customer)
        {
            this.customer = customer;
        }

        public void SetTransaction_Date(DateTime transactionDate)
        {
            this.transactionDate = transactionDate;
        }

        public void SetTransaction_Time(string transactionTime)
        {
            this.transactionTime = transactionTime;
        }

        public void SetTicket_Amount(int ticketAmount)
        {
            this.ticketAmount = ticketAmount;
        }

        public void SetTransaction_Amount(double transactionAmount)
        {
            this.transactionAmount = transactionAmount;
        }

        public string GetCustomer()
        {
            return customer;
        }

        public DateTime GetTransaction_Date()
        {
            return transactionDate;
        }

        public string GetTransaction_Time()
        {
            return transactionTime;
        }

        public int GetTicket_Amount()
        {
            return ticketAmount;
        }

        public double GetTransaction_Amount()
        {
            return transactionAmount;
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
            return $"Customer: {customer}\n" +
                   $"Transaction Date: {transactionDate.ToString("yyyy-MM-dd")}\n" +
                   $"Transaction Time: {transactionTime}\n" +
                   $"Ticket Amount: {ticketAmount}\n" +
                   $"Transaction Amount: {transactionAmount}";
        }
    }
}
