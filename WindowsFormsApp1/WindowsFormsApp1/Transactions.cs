using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {
                this.customer = customer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetTransaction_Date(DateTime transactionDate)
        {
            try
            {
                this.transactionDate = transactionDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetTransaction_Time(string transactionTime)
        {
            try
            {
                this.transactionTime = transactionTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetTicket_Amount(int ticketAmount)
        {
            try
            {
                this.ticketAmount = ticketAmount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetTransaction_Amount(double transactionAmount)
        {
            try
            {
                this.transactionAmount = transactionAmount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
