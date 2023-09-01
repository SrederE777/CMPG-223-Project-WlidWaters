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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

           
             
            
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            int margins = 20;
            List<Control> controls = new List<Control>();
            
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Bottom - GenericLooks.GetSize(typeof(Button)).Height - margins);
            controls.Add(GenericFunctions.CreateMenu(typeof(Button), location, this, "Exit"));
            GenericLooks.SetInputsLook(controls[0]);

            controls[0].Click += ExitClickedEvent;
        }

        private void ExitClickedEvent(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                
                Application.Exit();
            }
        }
    }
}
