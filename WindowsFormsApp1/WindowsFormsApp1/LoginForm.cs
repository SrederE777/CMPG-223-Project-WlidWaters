using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            

            if (login.Authenticate(username,password))
            {
                // Successful login
                //Employee currentUser = login.getValue();
                //MessageBox.Show("Login successful! Welcome, " + currentUser.Name);

                mainForm MainForm = new mainForm();
                MainForm.Show();
                this.Hide();
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                // Failed login
                MessageBox.Show("Login failed. Please check your username and password.");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
