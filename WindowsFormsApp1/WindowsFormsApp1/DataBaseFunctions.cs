using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp1
{
    public static class DataBaseFuncitons
    {

        private static string connectionString { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\32140274\Documents\CMPG 223\32140274EXAM\32140274EXAM\32140274EXAM\EventsDB.mdf;Integrated Security=True";
        private static SqlDataAdapter adap;
        private static SqlDataReader reader;
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static int currentUserID;
        private static string currentUser;
        private static bool premiumUser;
        public static void DisplayData(String sql, DataGridView display, SqlParameter[] parameters)
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


                            display.DataSource = ds.Tables[0];
                            //display.DataMember = table;
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
        public static int GetAndDisplayVenue(string sql, TextBox[] textBoxes, SqlParameter[] parameters)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    int VenueID;
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddRange(parameters);
                        using (reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            VenueID = reader.GetInt32(0);

                            sql = "SELECT * FROM Venue WHERE Id = @VenueId";
                        }
                    }
                    using (cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("VenueID", VenueID));
                        using (reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            textBoxes[1].Text = reader.GetString(1);
                            textBoxes[0].Text = reader.GetInt32(2).ToString();
                            con.Close();
                            return VenueID;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
                return -1;
            }
        }
        public static UpdateInfo.userInfo GetUserData()
        {
            try
            {
                //using using to ensure the the data connection closes
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @user", con))
                    {
                        cmd.Parameters.AddWithValue("@user", currentUser);

                        using (reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            UpdateInfo.userInfo info = new UpdateInfo.userInfo(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6).ToString(), reader.GetString(7));
                            con.Close();
                            return info;


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
                return null;
            }
        }
        public static void FormateData(string sql, SqlParameter[] parameters)
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
        public static bool IsPremium()
        {
            return premiumUser ? true : false;
        }
        public static void TogglePremium()
        {
            premiumUser = premiumUser ? false : true;
        }
        public static int GetUserID()
        {
            return currentUserID;
        }
        public static int GetCapacity(int id)
        {
            try
            {
                string sql = "SELECT Capacity FROM Venue WHERE Id = @id";
                SqlParameter parameter = new SqlParameter("@id", id);
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand(sql, con))
                    {

                        cmd.Parameters.Add(parameter);
                        using (reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            int output = reader.GetInt32(0);
                            con.Close();
                            return output;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
                return -1;
            }
        }
        public static bool CheckLogin(string user, string pass)
        {
            try
            {
                //using using to ensure the the data connection closes
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand("SELECT  Username, Password, Premium, Id FROM Users WHERE Username = @user", con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        currentUser = user;
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if ((string)reader.GetValue(1) == pass)
                                {
                                    premiumUser = reader.GetBoolean(2);
                                    currentUserID = reader.GetInt32(3);
                                    return true;
                                }
                            }
                        }
                    }
                    con.Close();
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err: " + ex.Message);
                return false;
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
    }
}
