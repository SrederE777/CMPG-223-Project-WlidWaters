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
    public partial class AssignEmp : Form
    {
        public AssignEmp()
        {
            InitializeComponent();
        }

        private void AssignEmp_Load(object sender, EventArgs e)
        {
            try
            {
                //display rides in datagrid
                DataBaseFuncitons dataAccess = new DataBaseFuncitons();

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
    }
}
