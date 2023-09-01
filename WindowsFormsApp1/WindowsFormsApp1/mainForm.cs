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
            this.Text = "FunTimeWaterPark";
            FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            MessageBox.Show(Screen.PrimaryScreen.WorkingArea.ToString());
            this.Show();


        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            int margins = 10;
            List<Control> controls = new List<Control>();
            
            //create the exit button
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Bottom - GenericLooks.GetSize(typeof(Button)).Height - margins);
            controls.Add(GenericFunctions.CreateMenuItem(typeof(Button), location, this, "Exit"));

            //create the menu
            List<string> MenuOptions = new List<string>
            {
                "Maintain Rides",
                "Maintain Employees",
                "Maintain Customers",
                "Sell Tickets",
                "Allocate Employees",
                "Reports"
            };

            Dictionary<string, EventHandler> buttonEvents = new Dictionary<string, EventHandler>
            {
                { "Exit", ExitClickedEvent }
            };



            location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Top  + margins);

            controls.AddRange(GenericFunctions.CreateMenu(MenuOptions, this, location, 4));

            foreach (Control control in controls)
            {
                if (control is Button button && buttonEvents.ContainsKey(button.Text))
                {
                    button.Click += buttonEvents[button.Text];
                }
            }


            Size screenSize = Screen.PrimaryScreen.Bounds.Size;

            
            float scaleFactor = (float)screenSize.Width / 1366; // My Current Rez to get it working
            

            // Scale the controls
            GenericLooks.ScaleControls(controls, scaleFactor);
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
