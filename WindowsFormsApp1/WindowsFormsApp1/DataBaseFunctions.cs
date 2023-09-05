using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace WindowsFormsApp1
{
    static class DataBaseFuncitons
    {
        public static string connectionString { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WildWatersDB.mdf;Integrated Security=True";
        private static SqlDataAdapter adap;
        private static SqlDataReader reader;
        private static SqlConnection con;
        private static SqlCommand cmd;
        

        public static void DisplayData(String sql, DataGridView display, SqlParameter[] parameters, string tablename)
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
                            adap.Fill(ds, tablename);


                            display.DataSource = ds;

                            display.DataMember = tablename;
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
    }
}
