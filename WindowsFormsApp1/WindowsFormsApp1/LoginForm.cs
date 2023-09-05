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

            bool isAuthenticated = login.Authenticate(username, password);

            if (isAuthenticated)
            {
                // Successful login
                Employee currentUser = login.getValue();
                MessageBox.Show("Login successful! Welcome, " + currentUser.Name);


                // Close the login form
                this.Close();
            }
            else
            {
                // Failed login
                MessageBox.Show("Login failed. Please check your username and password.");
            }
        }
    }
}
