using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btngenReports_Click(object sender, EventArgs e)
        {
            try
            {
                // Connection string for your SQL Server database
                string connectionString = "your_connection_string_here";

                // SQL query to retrieve transactions
                string sqlQuery = @"
                SELECT 
                    Transaction_Date, 
                    Transaction_Time, 
                    Transaction_Amount, 
                    Ride_ID, 
                    Customer_ID
                FROM 
                    Transactions
                ORDER BY 
                    Transaction_Date, 
                    Transaction_Time;
            ";

                // Create a StringBuilder to build the report
                StringBuilder report = new StringBuilder();

                // Report generated on
                report.AppendLine("Report generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                report.AppendLine(); // Empty line for separation
                report.AppendLine("Transaction history:");

                // Create a SQL Connection and Command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    DateTime currentDate = DateTime.MinValue;
                    decimal totalAmount = 0;

                    while (reader.Read())
                    {
                        DateTime transactionDate = reader.GetDateTime(0);
                        decimal transactionAmount = reader.GetDecimal(2);

                        // Check if a new date is encountered
                        if (transactionDate != currentDate)
                        {
                            if (currentDate != DateTime.MinValue)
                            {
                                // Display total amount for the previous date
                                report.AppendLine($"Total Transaction Amount for {currentDate:yyyy-MM-dd}: {totalAmount:C}");
                            }

                            currentDate = transactionDate;
                            totalAmount = 0;

                            // Display date
                            report.AppendLine(currentDate.ToString("yyyy-MM-dd"));
                        }

                        // Display transaction details
                        report.AppendLine($"Transaction ID: {reader.GetInt32(4)}");
                        report.AppendLine($"Transaction Time: {reader.GetTimeSpan(1)}");
                        report.AppendLine($"Transaction Amount: {transactionAmount:C}");
                        report.AppendLine($"Ride ID: {reader.GetInt32(3)}");
                        report.AppendLine($"Customer ID: {reader.GetInt32(5)}");
                        report.AppendLine(); // Empty line for separation

                        // Add transaction amount to the total
                        totalAmount += transactionAmount;
                    }

                    // Display total amount for the last date
                    report.AppendLine($"Total Transaction Amount for {currentDate:yyyy-MM-dd}: {totalAmount:C}");

                    // Close the database connection and populate the ListBox
                    connection.Close();
                }

                // Clear existing items in the ListBox
                lstTransactions.Items.Clear();

                // Split the report string by newlines and add each line to the ListBox
                foreach (string line in report.ToString().Split('\n'))
                {
                    lstTransactions.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
