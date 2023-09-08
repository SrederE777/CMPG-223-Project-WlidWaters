using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    static class DataBaseFuncitons
    {
        public static string connectionString { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WildWatersDB.mdf;Integrated Security=True";
        private static SqlDataAdapter adap;
        private static SqlDataReader reader;
        private static SqlConnection con;
        private static SqlCommand cmd;

        public static void Insert<T>(T obj, string tableName) where T : class, DataClasses
        {
            // Get the database column names from the object
            var columnNames = obj.getDataBaseName();

            // Build the INSERT statement
            var columns = string.Join(", ", columnNames.Select(c => $"[{c}]"));
            var values = string.Join(", ", columnNames.Select(c => $"@{c}"));
            var query = $"INSERT INTO [{tableName}] ({columns}) VALUES ({values})";

            // Create a new SqlConnection and SqlCommand
            using (con = new SqlConnection(connectionString))
            using (cmd = new SqlCommand(query, con))
            {
                // Add the parameter values
                foreach (var columnName in columnNames)
                {
                    //if (obj.GetType().GetProperty(columnName).GetType() != typeof(Rides))
                        cmd.Parameters.AddWithValue($"@{columnName}", obj.GetType().GetProperty(columnName).GetValue(obj));
                    //else
                    {
                        //string sql = "Select Ride_ID FROM Rides WHERE Ride_Name = @rideName AND Ride_Description = @rideDescription AND Ride_Availability = @rideAvailability AND Ride_Cost = @rideCost AND Ride_Length = @rideLength";

                    }
                    
                }

                // Open the connection and execute the command
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void DisplayData(String sql, DataGridView display, SqlParameter[] parameters, string tablename)
        {
            try
            {
                display.AllowUserToAddRows = false;
                display.AllowUserToDeleteRows =false;
                display.ReadOnly = true;
                display.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                

                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddRange(parameters);
                        using (adap = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            adap.Fill(ds, tablename);


                            display.DataSource = ds;

                            display.DataMember = tablename;
                        }
                    }

                    con.Close();
                }
                display.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
            }
        }

        public static T GetData<T>(string sql, SqlParameter[] parameters) where T : new()
        {
            try
            {
                List<T> output;
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddRange(parameters);
                        reader = cmd.ExecuteReader();
                        output = reader.Fill<T>();
                    }
                    con.Close();
                }

                if (output.Count() == 0)
                    return default(T);
                else
                    return output[0];
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return default(T);
            }
            

            
        }

        public static void ChangeData(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddRange(parameters);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
            }
        }
        public static void PopulateComboBox(string sql, ComboBox comboBox, SqlParameter[] parameters, string display, string value)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.AddRange(parameters);
                        using (adap = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            adap.Fill(ds);


                            comboBox.DataSource = ds.Tables[0];
                            comboBox.DisplayMember = display;
                            comboBox.ValueMember = value;
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
            }
        }

        public static void UpdateDatabase(string rideName, string employeeName)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand("UPDATE YourTableNameHere SET Employee_Name = @EmployeeName WHERE Ride_Name = @RideName", con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                        cmd.Parameters.AddWithValue("@RideName", rideName);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating database: " + ex.Message);
            }
        }

    }
}
