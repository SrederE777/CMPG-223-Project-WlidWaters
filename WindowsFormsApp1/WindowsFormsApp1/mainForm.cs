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
            try
            {
                //do this at the start of each new menu
                this.Controls.Clear();
                buttonEvents.Clear();

                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Bottom - GenericLooks.GetSize(typeof(Button)).Height - margins);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //This is working !!!
        private void GetEvents(string tag, List<string> MenuOptions)
        {
            try
            {
                List<string> searchOptions = MenuOptions.Select(t => t.Replace(" ", "")).ToList();



                MethodInfo[] eventinfos = typeof(mainForm).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

                int i = 0;
                foreach (MethodInfo eventinfo in eventinfos)
                {

                    foreach (string menuOption in searchOptions)
                    {
                        
                        if (eventinfo.Name.StartsWith(tag + menuOption))
                        {
                            
                            {
                                EventHandler temp = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), this, eventinfo.Name);
                                buttonEvents.Add(MenuOptions[i], temp);
                                i++;
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuMain()
        {
            try
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
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptions, this, location, 4);



                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMaintainMenuOperation<T>(string menuName) where T : class
        {
            try
            {
                List<string> MenuOptions = new List<string>
                {
                 menuName,
                "Enter " + menuName,
                };
                List<Type> MenuType = new List<Type>
                {
                typeof(Button),
                typeof(GroupBox)
                };

                Dictionary<string, Type> MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);
                //add Menu Events
                GetEvents(menuName.Replace(" ", ""), MenuOptions);

                //create menu
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptionType, this, location, 4);

                GenericFunctions.CreateInputs<T>(Controls.OfType<GroupBox>().FirstOrDefault(), -100, 5);

                MenuOptions.Clear();
                MenuType.Clear();
                MenuOptions.Add(menuName);
                MenuType.Add(typeof(DataGridView));

                MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);

                GetEvents("DataGridView", MenuOptions);

                //create menu
                location = new Point(Left + margins, Top + margins);

                GenericFunctions.CreateMenu(MenuOptionType, this, location);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuMaintainRide()
        {
            try
            {
                NewMenuStartCode();

                List<string> MenuOptions = new List<string>
            {
                "Add Ride",
                "Update Ride",
                "Delete Selected Ride"
            };

                //add Menu Events
                GetEvents("MenuMaintain", MenuOptions);

                //create menu
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuAddMaintainRide()
        {
            try
            {
                NewMenuStartCode();
                string menuName = "New Ride";
                NewMaintainMenuOperation<Rides>(menuName);
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuUpdateMaintainRide()
        {
            try
            {
                NewMenuStartCode();

                string menuName = "Add Ride";
                NewMaintainMenuOperation<Rides>(menuName); 
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuMaintainEmployees()
        {
            try
            {
                NewMenuStartCode();

                List<string> MenuOptions = new List<string>
                {
                "Add Employees",
                "Update Employees",
                "Delete Selected Employees"
                };

                //add Menu Events
                GetEvents("MenuMaintain", MenuOptions);

                //create menu
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
                NewMenuEndCode();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuAddMaintainEmployee()
        {
            try
            {
                NewMenuStartCode();

                string menuName = "New Employee";
                NewMaintainMenuOperation<Employee>(menuName);

                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuUpdateMaintainEmployee()
        {
            try
            {
                NewMenuStartCode();

                string menuName = "Current Employee";
                NewMaintainMenuOperation<Employee>(menuName);

                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuMaintainCustomers()
        {
            try
            {
                NewMenuStartCode();

                List<string> MenuOptions = new List<string>
                {
                "Add Customers",
                "Update Customers",
                "Delete Selected Customers"
                };

                //add Menu Events
                GetEvents("MenuMaintain", MenuOptions);

                //create menu
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuAddMaintainCustomers()
        {
            try
            {
                NewMenuStartCode();

                String menuName = "Current Customers";
                NewMaintainMenuOperation<Customer>(menuName);

                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuUpdateMaintainCustomers()
        {
            try
            {
                NewMenuStartCode();
                String menuName = "Add Customers";
                NewMaintainMenuOperation<Customer>(menuName);
                NewMenuEndCode();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuSellTicketsTransation()
        {
            try
            {
                NewMenuStartCode();
                string menuName = "Sell Tickets";
                NewMaintainMenuOperation<Transactions>(menuName);
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMenuRequestReports()
        {
            NewMenuStartCode();
            List<string> MenuOptions = new List<string>
            {

            };
            GetEvents("Reports", MenuOptions);
            NewMenuEndCode();
            GenericFunctions.CreateForm<Reports>("Reports", this);
            
            
            
            
        }

        private void NewMenuEndCode()
        {
            try
            {
                //do this for the end of each menu
                foreach (Control control in Controls)
                {
                    if (control is Button button && buttonEvents.ContainsKey(button.Text))
                    {
                        button.Click += (sender, e) =>
                        {

                            buttonEvents[button.Text]?.Invoke(this, EventArgs.Empty);

                        };
                    }
                }

                Size screenSize = Screen.PrimaryScreen.Bounds.Size;


                float scaleFactor = (float)screenSize.Width / 1366; // My Current Rez to get it working


                // Scale the controls
                GenericLooks.ScaleControls(Controls, scaleFactor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public mainForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //this is how the form looks
                Text = "FunTimeWaterPark";
                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.CenterScreen;
                WindowState = FormWindowState.Maximized;
                ControlBox = false;
                //MessageBox.Show(Screen.PrimaryScreen.WorkingArea.ToString());
                Show();


                // Get all the private instance methods of the current type
                MethodInfo[] methods = typeof(mainForm).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

                // Loop through each method
                foreach (MethodInfo method in methods)
                {
                    // Get the name of the method
                    string name = method.Name;
                    MenuDelegate menuDelegate;
                    Type delegateType = typeof(MenuDelegate);
                    Type[] neededParameterTypes = delegateType.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType).ToArray();
                    Type[] actualParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
                    // Check if the name starts with "NewMenu"
                    if (name.StartsWith("NewMenu") && neededParameterTypes.SequenceEqual(actualParameterTypes))
                    {


                        // Create an instance of the delegate and assign it to the method
                        menuDelegate = (MenuDelegate)Delegate.CreateDelegate(typeof(MenuDelegate), this, method);

                        // Add the delegate to the dictionary with the name as the key
                        menuDictionary.Add(name, menuDelegate);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                NewMenuMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BackClickedEvent(object sender, EventArgs e)
        {
            try
            {
                navigationHistory.Pop();
                string previousMenu = navigationHistory.Pop();
                menuDictionary[previousMenu].DynamicInvoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExitClickedEvent(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuMaintainRidesEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuMaintainRide();
                //Control controls = 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainAddRideEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuAddMaintainRide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NewRideNewRideEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);
                Rides ride = GenericFunctions.CreateObjectFromControls<Rides>(controls.ToArray());
                MessageBox.Show(ride.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CurrentRideCurrentRideEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);

                Rides ride = GenericFunctions.CreateObjectFromControls<Rides>(controls.ToArray());
                MessageBox.Show(ride.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MenuMaintainUpdateRideEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuUpdateMaintainRide();
                List<Control> controls = GenericFunctions.getInputs(this);
                Rides ride = new Rides("Test",false,100,10,"Test");
                GenericFunctions.PopulateControlsFromObject(controls.ToArray(), ride);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainDeleteRideEvent(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuMaintainEmployeesEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuMaintainEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainAddEmployeesEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuAddMaintainEmployee();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        

        private void NewEmployeeNewEmployeeEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);
                Employee employee = GenericFunctions.CreateObjectFromControls<Employee>(controls.ToArray());
                MessageBox.Show(employee.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CurrentEmployeeCurrentEmployeeEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);

                Employee employee = GenericFunctions.CreateObjectFromControls<Employee>(controls.ToArray());
                MessageBox.Show(employee.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MenuMaintainUpdateEmployeesEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuUpdateMaintainEmployee();
                List<Control> controls = GenericFunctions.getInputs(this);
                Employee employee = new Employee("Test", "Test", DateTime.Now, "Test", "Test,", " Test");

                GenericFunctions.PopulateControlsFromObject(controls.ToArray(), employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NewCustomerNewCustomerEvent(object sender, EventArgs e)
        {
            try
            {


                List<Control> controls = GenericFunctions.getInputs(this);

                Customer customer = GenericFunctions.CreateObjectFromControls<Customer>(controls.ToArray());
                MessageBox.Show(customer.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CurrentCustomersCurrentCustomersEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);
                Customer customer = GenericFunctions.CreateObjectFromControls<Customer>(controls.ToArray());
                MessageBox.Show(customer.ToString());

                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MenuMaintainUpdateCustomersEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuUpdateMaintainCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainDeleteEmployeesEvent(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuMaintainCustomersEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuMaintainCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainAddCustomersEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuAddMaintainCustomers();
                List<Control> controls = GenericFunctions.getInputs(this);
                Customer myObject = new Customer("Test", "Test", DateTime.Now, "Test");
                GenericFunctions.PopulateControlsFromObject(controls.ToArray(), myObject);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        

        

        private void MenuMaintainDeleteCustomersEvent(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuSellTicketsEvent(object sender, EventArgs e)
        {
            try
            {
                NewMenuSellTicketsTransation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuAllocateEmployeesEvent(object sender, EventArgs e)
        {
            //NewMenuRequestReports();
        }

        bool flagtest = true;
        private void MainMenuReportsEvent(object sender, EventArgs e)
        {
            if (flagtest)
            {
                NewMenuRequestReports();
                flagtest = false;
            }
            else
            {
                flagtest = true;
            }

        }
    }
}
