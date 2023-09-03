using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;


namespace WindowsFormsApp1
{
    public partial class mainForm : Form
    {
        Stack<string> navigationHistory = new Stack<string>();

        private const int margins = 10;
        
        private Dictionary<string, EventHandler> buttonEvents = new Dictionary<string, EventHandler>();

        private Dictionary<string, Delegate> menuDictionary = new Dictionary<string, Delegate>();

        delegate void MenuDelegate();
        delegate void EventHandlerFunction(object sender, EventArgs e);

        private void NewMenuStartCode()
        {
            //do this at the start of each new menu
            this.Controls.Clear();
            buttonEvents.Clear();

            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Bottom - GenericLooks.GetSize(typeof(Button)).Height - margins);
            if (navigationHistory.Count == 0)
            {
                buttonEvents.Add("Exit", ExitClickedEvent);
                GenericFunctions.CreateMenuItem(typeof(Button), location, this, "Exit");
            }
            else
            {
                buttonEvents.Add("Back", BackClickedEvent);
                GenericFunctions.CreateMenuItem(typeof(Button), location, this, "Back");
            }

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            string callingName = stackFrame.GetMethod().Name;



            navigationHistory.Push(callingName);
            //This should add all the methods to the dictionary which should allow the stack to basically just pop and create the right form



        }

        //This is working !!!
        private void GetEvents(string tag, List<string> MenuOptions)
        {
            List<string> searchOptions = MenuOptions.Select(t => t.Replace(" ", "")).ToList();



            MethodInfo[] eventinfos = typeof(mainForm).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            int i = 0;
            foreach ( MethodInfo eventinfo in eventinfos)
            {
                
                foreach (string menuOption in searchOptions)
                {
                    if (eventinfo.Name.StartsWith(tag + menuOption))
                    {
                        //if (!buttonEvents.ContainsKey(MenuOptions[i]))
                        {
                            EventHandler temp = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), this, eventinfo.Name);
                            buttonEvents.Add(MenuOptions[i], temp);
                            i++;
                        }
                        
                    }
                }
                
            }
        }
        private void NewMenuMain()
        {
            NewMenuStartCode();

            //create the menu options
            List<string> MenuOptions = new List<string>
            {
                "Maintain Rides",
                "Maintain Employees", 
                "Maintain Customers",
                "Sell Tickets",
                "Allocate Employees",
                "Reports"
            };

            //subscribes to the events named acccording to the naming convention
            GetEvents("MainMenu", MenuOptions);


            //create menu
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Top + margins);
            GenericFunctions.CreateMenu(MenuOptions, this, location, 4);

            

            NewMenuEndCode();
        }

        private void NewMenuMaintainRide()
        {
            NewMenuStartCode();

            List<string> MenuOptions = new List<string>
            {
                "Add Ride",
                "Update Ride",
                "Delete Ride"
            };

            //add Menu Events
            GetEvents("MenuMaintain", MenuOptions);

            //create menu
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Top + margins);
            GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
            NewMenuEndCode();
        }

        private void NewMenuMaintainEmployees()
        {
            NewMenuStartCode();

            List<string> MenuOptions = new List<string>
            {
                "Add Employees",
                "Update Employees",
                "Delete Employees"
            };

            //add Menu Events
            GetEvents("MenuMaintain", MenuOptions);

            //create menu
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Top + margins);
            GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
            NewMenuEndCode();
        }

        private void NewMenuMaintainCustomers()
        {
            NewMenuStartCode();

            List<string> MenuOptions = new List<string>
            {
                "Add Employees",
                "Update Employees",
                "Delete Employees"
            };

            //add Menu Events
            GetEvents("MenuMaintain", MenuOptions);

            //create menu
            Point location = new Point(this.Right - GenericLooks.GetSize(typeof(Button)).Width - margins, this.Top + margins);
            GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
            NewMenuEndCode();
        }

        private void NewMenuEndCode()
        {

            //do this for the end of each menu
            foreach (Control control in Controls)
            {
                if (control is Button button && buttonEvents.ContainsKey(button.Text))
                {
                    button.Click += buttonEvents[button.Text];
                }
            }

            Size screenSize = Screen.PrimaryScreen.Bounds.Size;


            float scaleFactor = (float)screenSize.Width / 1366; // My Current Rez to get it working


            // Scale the controls
            GenericLooks.ScaleControls(Controls, scaleFactor);
        }

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
            //this is how the form looks
            this.Text = "FunTimeWaterPark";
            FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
            //MessageBox.Show(Screen.PrimaryScreen.WorkingArea.ToString());
            this.Show();
            

            // Get all the private instance methods of the current type
            MethodInfo[] methods = typeof(mainForm).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            // Loop through each method
            foreach (MethodInfo method in methods)
            {
                // Get the name of the method
                string name = method.Name;

                // Check if the name starts with "NewMenu"
                if (name.StartsWith("NewMenu"))
                {
                    // Create an instance of the delegate and assign it to the method
                    MenuDelegate menuDelegate = (MenuDelegate)Delegate.CreateDelegate(typeof(MenuDelegate), this, method);

                    // Add the delegate to the dictionary with the name as the key
                    menuDictionary.Add(name, menuDelegate);
                }
            }


        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            NewMenuMain();
        }

        private void BackClickedEvent(object sender, EventArgs e)
        {
            navigationHistory.Pop();
            string previousMenu = navigationHistory.Pop();
            menuDictionary[previousMenu].DynamicInvoke();
        }

        private void ExitClickedEvent(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                
                Application.Exit();
            }
        }

        private void MainMenuMaintainRidesEvent(object sender, EventArgs e)
        {
            NewMenuMaintainRide();
        }

        private void MenuMaintainAddRideEvent(object sender, EventArgs e)
        {

        }

        private void MenuMaintainUpdateRideEvent(object sender, EventArgs e)
        {

        }

        private void MenuMaintainDeleteRideEvent(object sender, EventArgs e)
        {

        }

        private void MainMenuMaintainEmployeesEvent(object sender, EventArgs e)
        { 
            NewMenuMaintainEmployees();
        }

        private void MenuMaintainAddEmployeesEvent(object sender, EventArgs e)
        {

        }

        private void MenuMaintainUpdateEmployeesEvent(object sender, EventArgs e)
        {

        }

        private void MenuMaintainDeleteEmployeesEvent(object sender, EventArgs e)
        {

        }

        private void MainMenuMaintainCustomersEvent(object sender, EventArgs e)
        {
            NewMenuMaintainCustomers();
        }
    }
}
