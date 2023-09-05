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
        // Function to retrieve ride name based on Ride_ID (replace with your table and column names)
        private string GetRideName(int rideID, SqlConnection connection)
        {
            string sqlQuery = @"
                SELECT 
                    Ride_Name
                FROM 
                    Rides
                WHERE 
                    Ride_ID = @RideID;
                ";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@RideID", rideID);
                object result = command.ExecuteScalar();
                return result != null ? result.ToString() : "Unknown Ride";
            }
        }

        public void getTransactionReports()
        {
            try
            {
                // Connection string for your SQL Server database
                string connectionString = DataBaseFuncitons.connectionString;


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

        public void getRidesReport()
        {
            try
            {
                // Connection string for your SQL Server database
                string connectionString = DataBaseFuncitons.connectionString;





                // SQL query to retrieve popular rides
                string sqlQuery = @"
                SELECT 
                    Ride_ID, 
                    COUNT(*) AS RideCount
                FROM 
                    Transactions
                GROUP BY 
                    Ride_ID
                ORDER BY 
                    RideCount DESC;
            ";

                // Create a StringBuilder to build the report
                StringBuilder report = new StringBuilder();

                // Report generated on
                report.AppendLine("Report generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                report.AppendLine(); // Empty line for separation

                // Title for popular rides
                report.AppendLine("Popular rides:");

                // Create a SQL Connection and Command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int rideID = reader.GetInt32(0);
                        int rideCount = reader.GetInt32(1);

                        // Retrieve the ride name from the rides table (replace with your table name)
                        string rideName = GetRideName(rideID, connection);

                        // Display the ride name and ride count
                        report.AppendLine($"Ride Name: {rideName}");
                        report.AppendLine($"Ride Count: {rideCount}");
                        report.AppendLine(); // Empty line for separation
                    }

                    // Close the database connection
                    connection.Close();
                }

                // Clear existing items in ListBox2
                lstPopularRides.Items.Clear();

                // Split the report string by newlines and add each line to ListBox2
                foreach (string line in report.ToString().Split('\n'))
                {
                    lstPopularRides.Items.Add(line);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getEmployeeReports()
        {
            try
            {
                // Connection string for your SQL Server database
                string connectionString = DataBaseFuncitons.connectionString;


                // SQL query to retrieve employee ride data
                string sqlQuery = @"
                SELECT 
                E.Employee_Name, 
                E.Employee_Surname, 
                R.Ride_Name
                FROM 
                Employees AS E
                INNER JOIN 
                Rides AS R ON E.Ride_ID = R.Ride_ID;

            ";

                // Create a StringBuilder to build the report
                StringBuilder report = new StringBuilder();

                // Report generated on
                report.AppendLine("Report generated on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                report.AppendLine(); // Empty line for separation

                // Title for Employee Report
                report.AppendLine("Employee Report:");

                // Create a SQL Connection and Command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string employeeName = reader.GetString(0);
                        string employeeSurname = reader.GetString(1);
                        string rideName = reader.GetString(2);

                        // Display Employee_Name, Employee_Surname, and Ride_Name
                        report.AppendLine($"Employee Name: {employeeName}");
                        report.AppendLine($"Employee Surname: {employeeSurname}");
                        report.AppendLine($"Ride Name: {rideName}");
                        report.AppendLine(); // Empty line for separation
                    }

                    // Close the database connection
                    connection.Close();
                }

                // Clear existing items in ListBox3
                lstEmployeeReport.Items.Clear();

                // Split the report string by newlines and add each line to ListBox3
                foreach (string line in report.ToString().Split('\n'))
                {
                    lstEmployeeReport.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                getTransactionReports();
                getRidesReport();
                getEmployeeReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void Reports_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm form = (mainForm)this.Owner;
            form.BackClickedEvent(this, EventArgs.Empty);
        }
    }
}
