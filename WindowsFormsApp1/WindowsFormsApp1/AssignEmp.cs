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
    public partial class c : Form
    {


        public c()
        {
            InitializeComponent();
        }

        private void AssignEmp_Load(object sender, EventArgs e)
        {
            try
            {
                //display rides in datagrid
                

                string sqlQuery = "SELECT * FROM Rides";
                SqlParameter[] parameters = new SqlParameter[0]; 

                DataBaseFuncitons.DisplayData(sqlQuery, dataGridView1, parameters, "Rides");


                //popualate combobox rides
                
                string sql2 = "SELECT Ride_ID, Ride_Name FROM Rides"; 
                
                SqlParameter[] parameters2 = null;

                DataBaseFuncitons.PopulateComboBox(sql2, cmbRideName, parameters2, "Ride_Name", "Ride_ID");

                cmbRideName.SelectedIndex = 0;

                //populate combobox employees
                string sql3 = "SELECT Employee_Id, Employee_Name FROM Employees"; 


                SqlParameter[] parameters3 = null;

                DataBaseFuncitons.PopulateComboBox(sql3, cmbRideName, parameters3, "Employee_Name", "Employee_ID");

                cmbRideName.SelectedIndex = 0;
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AssignEmp_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm form = (mainForm)this.Owner;
            form.BackClickedEvent(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedRideName = cmbRideName.Text;
                string selectedEmployeeName = cmbEmployeeName.Text;

                // Iterate through the DataGridView rows to find the matching ride name
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Ride_Name"].Value != null &&
                        row.Cells["Ride_Name"].Value.ToString() == selectedRideName)
                    {
                        // Update the employee name in the DataGridView
                        row.Cells["Employee_Name"].Value = selectedEmployeeName;

                        // You may want to implement SQL update logic here to update the database
                        DataBaseFuncitons.UpdateDatabase(selectedRideName, selectedEmployeeName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
