﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Data.SqlClient;


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

                List<string> MenuOptions;
                //create the menu options
                if (login.getAccessLevel() == 0)
                {
                    MenuOptions = new List<string>
                    {
                    "Maintain Rides",
                    "Maintain Employees",
                    "Maintain Customers",
                    "Sell Tickets",
                    "Allocate Employees",
                    "Reports"
                    };
                }
                else if (login.getAccessLevel() == 1)
                {
                    MenuOptions = new List<string>
                    {
                    "Maintain Customers",
                    "Sell Tickets"
                    };
                }
                else
                {
                    MenuOptions = new List<string>
                    {
                    };
                }
                




                //subscribes to the events named acccording to the naming convention
                GetEvents("MainMenu", MenuOptions);


                //create menu
                Point location = new Point(Right - GenericLooks.GetSize(typeof(Button)).Width - margins, Top + margins);
                GenericFunctions.CreateMenu(MenuOptions, this, location, 4);
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "OIG.jpeg");
                // Create a new PictureBox
                PictureBox pictureBox = new PictureBox
                {
                    
                    Image = Image.FromFile(imagePath),

                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.Fill
                };

                // Add the PictureBox to your form
                this.Controls.Add(pictureBox);


                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewMaintainMenuOperation<T>(string menuName) where T : class , DataClasses
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

                List<Type> MenuType = new List<Type>
                {
                typeof(Button),
                typeof(GroupBox)
                };
                MenuOptions.Clear();
                MenuType.Clear();
                MenuOptions.Add("DataGridView");
                MenuType.Add(typeof(DataGridView));

                Dictionary<string, Type> MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);

                GetEvents("DataGridView", MenuOptions);

                //create menu
                location = new Point(Left + margins, Top + margins);
                DataGridView dataGridView = null;
                List<Control> datagrid = GenericFunctions.CreateMenu(MenuOptionType, this, location);
                foreach (Control control in datagrid)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Rides";
                SqlParameter[] parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Rides");
                dataGridView.CellClick += SelectionChangedOnTheDataGridViewRides;
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
                DataGridView dataGridView = null;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Rides";
                    SqlParameter[] parameters = new SqlParameter[0];

                    DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Rides");
                

                
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

                string menuName = "Change Ride";
                NewMaintainMenuOperation<Rides>(menuName);
                DataGridView dataGridView = null;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Rides";
                SqlParameter[] parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Rides");
                
                dataGridView.CellClick += SelectionChangedOnTheDataGridViewRides;
                
                


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

                List<Type> MenuType = new List<Type>
                {
                typeof(Button),
                typeof(GroupBox)
                };
                MenuOptions.Clear();
                MenuType.Clear();
                MenuOptions.Add("DataGridView");
                MenuType.Add(typeof(DataGridView));

                Dictionary<string, Type> MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);

                GetEvents("DataGridView", MenuOptions);

                //create menu
                location = new Point(Left + margins, Top + margins);
                DataGridView dataGridView = null;
                List<Control> datagrid = GenericFunctions.CreateMenu(MenuOptionType, this, location);
                string sql;
                SqlParameter[] parameters;
                foreach (Control control in datagrid)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                    
                }
                sql = "SELECT Employees.Employee_ID, Employees.Employee_Name, Employee_Surname, Employee_Emergency_Contact, Employee_Contact, Rides.Ride_Name, Rides.Ride_ID FROM Employees JOIN Rides ON Employees.Ride_ID = Rides.Ride_ID";
                parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Employees");
                dataGridView.Columns["Ride_ID"].Visible = false;

                dataGridView.CellClick += SelectionChangedOnTheDataGridViewEmployee;
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
                string sql = "";
                string menuName = "New Employee";
                SqlParameter[] parameters = new SqlParameter[0];
                NewMaintainMenuOperation<Employee>(menuName);
                DataGridView dataGridView = null;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        
                    }
                    else if (control is GroupBox)
                    {
                        GroupBox inputBox = (GroupBox)control;
                        ComboBox[] combo = inputBox.Controls.OfType<ComboBox>().ToArray(); 
                        if (combo != null)
                        {
                            //combo[0].Items.Clear();
                            sql = "SELECT Ride_Name, Ride_ID FROM Rides";
                            parameters = new SqlParameter[] { };
                            DataBaseFuncitons.PopulateComboBox(sql, combo[0], parameters, "Ride_Name", "Ride_ID");
                        }
                        

                    }
                }
                sql = "SELECT Employees.Employee_ID, Employees.Employee_Name, Employee_Surname, Employee_Emergency_Contact, Employee_Contact, Rides.Ride_Name , Rides.Ride_ID FROM Employees JOIN Rides ON Employees.Ride_ID = Rides.Ride_ID";
                parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Employees");
                dataGridView.Columns["Ride_ID"].Visible = false;
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

                string menuName = "Update Employee";
                NewMaintainMenuOperation<Employee>(menuName);
                DataGridView dataGridView = null;
                string sql;
                SqlParameter[] parameters;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                    }
                    else if (control is GroupBox)
                    {
                        GroupBox inputBox = (GroupBox)control;
                        ComboBox[] combo = inputBox.Controls.OfType<ComboBox>().ToArray();
                        if (combo != null)
                        {
                            //combo[0].Items.Clear();
                            sql = "SELECT Ride_Name, Ride_ID FROM Rides";
                            parameters = new SqlParameter[] { };
                            DataBaseFuncitons.PopulateComboBox(sql, combo[0], parameters, "Ride_Name", "Ride_ID");
                        }


                    }
                }
                sql = "SELECT Employees.Employee_ID, Employees.Employee_Name, Employee_Surname, Employee_Emergency_Contact, Employee_Contact, Rides.Ride_Name, Rides.Ride_ID FROM Employees JOIN Rides ON Employees.Ride_ID = Rides.Ride_ID";
                parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Employees");
                dataGridView.Columns["Ride_ID"].Visible = false;
                dataGridView.CellClick += SelectionChangedOnTheDataGridViewEmployee;
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
                List<Type> MenuType = new List<Type>
                {
                typeof(Button),
                typeof(GroupBox)
                };
                MenuOptions.Clear();
                MenuType.Clear();
                MenuOptions.Add("DataGridView");
                MenuType.Add(typeof(DataGridView));

                Dictionary<string, Type> MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);

                GetEvents("DataGridView", MenuOptions);

                //create menu
                location = new Point(Left + margins, Top + margins);
                DataGridView dataGridView = null;
                List<Control> datagrid = GenericFunctions.CreateMenu(MenuOptionType, this, location);
                foreach (Control control in datagrid)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Customers";
                SqlParameter[] parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Customers");
                dataGridView.CellClick += SelectionChangedOnTheDataGridViewCustomer;
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
                DataGridView dataGridView = null;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Customers";
                SqlParameter[] parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Customers");
                dataGridView.CellClick += SelectionChangedOnTheDataGridViewCustomer;
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
                DataGridView dataGridView = null;
                foreach (Control control in Controls)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                string sql = "SELECT * FROM Customers";
                SqlParameter[] parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Customers");
                
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
                GroupBox inputGroupBox = Controls.OfType<GroupBox>().FirstOrDefault();
                List<Control> controls = GenericFunctions.CreateInputs<Transactions>(inputGroupBox, -100, 5);
                string sql = "SELECT * FROM Transactions";
                SqlParameter[] parameters = new SqlParameter[0];
                foreach (Control control in controls)
                {
                    if (control is DateTimePicker)
                    {
                        DateTimePicker date = (DateTimePicker)control;
                        date.Value = DateTime.Now;
                        date.Enabled = false;
                    }
                    else if (control is TextBox)
                    {
                        TextBox text = (TextBox)control;
                        text.Text = DateTime.Now.TimeOfDay.ToString();
                        text.Enabled = false;
                    }
                    else if (control is NumericUpDown && control == controls[5])
                    {
                        control.Hide();
                        Label label = inputGroupBox.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "Transaction Amount");
                        label.Hide();
                    }
                    else if (control is ComboBox && control.Name.Contains("Customer"))
                    {
                        ComboBox combo = (ComboBox)control;
                        sql = "SELECT Customer_Name, Customer_ID FROM Customers";
                        parameters = new SqlParameter[] { };
                        DataBaseFuncitons.PopulateComboBox(sql, combo, parameters, "Customer_Name", "Customer_ID");
                        combo.DropDownStyle = ComboBoxStyle.DropDown;
                        combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        combo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    }
                    else if (control is ComboBox && control.Name.Contains("Ride"))
                    {
                        ComboBox combo = (ComboBox)control;
                        sql = "SELECT Ride_Name, Ride_ID FROM Rides";
                        parameters = new SqlParameter[] { };
                        DataBaseFuncitons.PopulateComboBox(sql, combo, parameters, "Ride_Name", "Ride_ID");
                        combo.DropDownStyle = ComboBoxStyle.DropDown;
                        combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        combo.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }

                MenuOptions.Clear();
                MenuType.Clear();
                MenuOptions.Add("DataGridView");
                MenuType.Add(typeof(DataGridView));

                MenuOptionType = MenuOptions.Zip(MenuType, (k, v) => new { Key = k, Value = v })
                                                 .ToDictionary(x => x.Key, x => x.Value);

                GetEvents("DataGridView", MenuOptions);

                //create menu
                location = new Point(Left + margins, Top + margins);
                DataGridView dataGridView = null;
                List<Control> datagrid = GenericFunctions.CreateMenu(MenuOptionType, this, location);
                foreach (Control control in datagrid)
                {
                    if (control is DataGridView)
                    {
                        dataGridView = (DataGridView)control;
                        break;
                    }
                }
                sql = "SELECT Transactions.Transaction_ID, Customers.Customer_Name AS Customer_Name, Rides.Ride_Name AS Ride_Name, Transactions.Transaction_Date, Transactions.Transaction_Time, Transactions.Transaction_Amount " +
                    "FROM Transactions " +
                    "INNER JOIN Customers ON Transactions.Customer_ID = Customers.Customer_ID " +
                    "INNER JOIN Rides ON Transactions.Ride_ID = Rides.Ride_ID";


                parameters = new SqlParameter[0];

                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Transactions");
                NewMenuEndCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                NewMenuEndCode();
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

        private void NewMenuAllocateEmployees()
        {
            NewMenuStartCode();
            List<string> MenuOptions = new List<string>
            {

            };
            GetEvents("AllocateEmployee", MenuOptions);
            NewMenuEndCode();
            GenericFunctions.CreateForm<c>("Reports", this);
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
                MessageBox.Show("Adding the following ride: \n" + ride.ToString());
                DataBaseFuncitons.Insert<Rides>(ride, "Rides");
                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeRideChangeRideEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);

                Rides ride = GenericFunctions.CreateObjectFromControls<Rides>(controls.ToArray());
                MessageBox.Show("Chaning the following ride: \n" + ride.ToString());
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int ride_ID = Convert.ToInt32(selectedRow.Cells["Ride_ID"].Value.ToString());
                    string sql = "UPDATE Rides " + "SET Ride_Description = @RideDescription, " +
                                             "Ride_Name = @RideName, " +
                                          "    Ride_Availability = @RideAvailability, " +
                                          "    Ride_Cost = @RideCost, " +
                                          "    Ride_Length = @RideLength " +
                                          "WHERE Ride_ID = @RideID";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@RideDescription", ride.Ride_Description),
                    new SqlParameter("@RideAvailability", ride.Ride_Availability),
                    new SqlParameter("@RideCost", ride.Ride_Cost),
                    new SqlParameter("@RideLength", ride.Ride_Length),
                    new SqlParameter("@RideName", ride.Ride_Name),
                    new SqlParameter("@RideID", ride_ID)
                    };
                    DataBaseFuncitons.ChangeData(sql, parameters);

                    BackClickedEvent(this, EventArgs.Empty);
                }
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
                
                
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    Rides ride = new Rides()
                    {
                        Ride_Name = selectedRow.Cells["Ride_Name"].Value.ToString(),
                        Ride_Description = selectedRow.Cells["Ride_Description"].Value.ToString(),
                        Ride_Availability = Convert.ToBoolean(selectedRow.Cells["Ride_Availability"].Value.ToString()),
                        Ride_Cost = Convert.ToInt32(selectedRow.Cells["Ride_Cost"].Value.ToString()),
                        Ride_Length = Convert.ToInt32(selectedRow.Cells["Ride_Length"].Value.ToString()),
                        
                    };
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), ride);
                }
                else
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), new Rides());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainDeleteSelectedRideEvent(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    
                    int ID = Convert.ToInt32(selectedRow.Cells["Ride_ID"].Value.ToString());
                    string name = selectedRow.Cells["Ride_Name"].Value.ToString();
                    DialogResult result = MessageBox.Show("Are you sure you want to delete the "+ name + " Ride? (Note this will unassign all employess assigned to the ride)", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string sql = "UPDATE Employees SET Ride_ID = NULL WHERE Ride_ID = @Rides_ID";
                        SqlParameter[] parameters = { new SqlParameter("@Rides_ID", ID) };
                        DataBaseFuncitons.ChangeData(sql, parameters);

                        sql = "DELETE FROM Rides WHERE Ride_ID = @Rides_ID";
                        parameters = new SqlParameter[1] { new SqlParameter("@Rides_ID", ID) };
                        DataBaseFuncitons.ChangeData(sql, parameters);

                        sql = "Select * FROM Rides ";
                        parameters = new SqlParameter[0];
                        DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Rides");

                    }
                }
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
                //event that happens when the button that should add a new employee is pressed
                List<Control> controls = GenericFunctions.getInputs(this);
                Employee employee = GenericFunctions.CreateObjectFromControls<Employee>(controls.ToArray());
                MessageBox.Show(employee.ToString());

                string sql = "INSERT INTO Employees(Employee_Name, Employee_Surname, Employee_Emergency_Contact, Employee_Contact, Employee_Password, Ride_ID) VALUES(@Employee_Name, @Employee_Surname, @Employee_Emergency_Contact, @Employee_Contact, @Employee_Password, @Ride_ID)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Employee_Name", employee.Employee_Name),
                    new SqlParameter("@Employee_Surname", employee.Employee_Surname),
                    new SqlParameter("@Employee_Emergency_Contact", employee.Employee_Emergency_Contact),
                    new SqlParameter("@Employee_Contact", employee.Employee_Contact),
                    new SqlParameter("@Employee_Password", employee.Employee_Password),
                    new SqlParameter("@Ride_ID", employee.Ride_ID.value)
                };
                DataBaseFuncitons.ChangeData(sql, parameters);
                BackClickedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateEmployeeUpdateEmployeeEvent(object sender, EventArgs e)
        {
            try
            {
                //button that updates a employee is pressed
                List<Control> controls = GenericFunctions.getInputs(this);

                Employee employee = GenericFunctions.CreateObjectFromControls<Employee>(controls.ToArray());
                MessageBox.Show("Chaning the following Employee: \n" + employee.ToString());
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int employee_ID = Convert.ToInt32(selectedRow.Cells["Employee_ID"].Value.ToString());
                    string sql;
                    SqlParameter[] parameters;
                    if (employee.Employee_Password != "")
                    {
                        sql = "UPDATE Employees SET Employee_Name = @Employee_Name, " +
                                          "    Employee_Surname = @Employee_Surname, " +
                                          "    Employee_Emergency_Contact = @Employee_Emergency_Contact, " +
                                          "    Employee_Contact = @Employee_Contact, " +
                                          "    Employee_Password = @Employee_Password, "+
                                          "    Ride_ID = @Ride_ID " +
                                          "WHERE Employee_ID = @EmployeeID";
                        parameters = new SqlParameter[]
                        {
                        new SqlParameter("@Employee_Name", employee.Employee_Name),
                        new SqlParameter("@Employee_Surname", employee.Employee_Surname),
                        new SqlParameter("@Employee_Emergency_Contact", employee.Employee_Emergency_Contact),
                        new SqlParameter("@Employee_Contact", employee.Employee_Contact),
                        new SqlParameter("@Employee_Password", employee.Employee_Password),
                        new SqlParameter("@Ride_ID", employee.Ride_ID.value),
                        new SqlParameter("@EmployeeID", employee_ID)
                        };
                        DataBaseFuncitons.ChangeData(sql, parameters);
                    }

                    else
                    {
                        sql = "UPDATE Employees " + "SET Employee_Name = @Employee_Name, " +
                                          "    Employee_Surname = @Employee_Surname, " +
                                          "    Employee_Emergency_Contact = @Employee_Emergency_Contact, " +
                                          "    Employee_Contact = @Employee_Contact, " +
                                          "    Ride_ID = @Ride_ID " +
                                          "WHERE Employee_ID = @EmployeeID";
                        parameters = new SqlParameter[]
                        {
                        new SqlParameter("@Employee_Name", employee.Employee_Name),
                        new SqlParameter("@Employee_Surname", employee.Employee_Surname),
                        new SqlParameter("@Employee_Emergency_Contact", employee.Employee_Emergency_Contact),
                        new SqlParameter("@Employee_Contact", employee.Employee_Contact),
                        new SqlParameter("@Ride_ID", employee.Ride_ID.value),
                        new SqlParameter("@EmployeeID", employee_ID)
                        };
                        DataBaseFuncitons.ChangeData(sql, parameters);
                    }

                    

                    BackClickedEvent(this, EventArgs.Empty);
                }
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


                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    Employee employee = new Employee()
                    {
                        Employee_Name = selectedRow.Cells["Employee_Name"].Value.ToString(),
                        Employee_Surname = selectedRow.Cells["Employee_Surname"].Value.ToString(),
                        Employee_Emergency_Contact = selectedRow.Cells["Employee_Emergency_Contact"].Value.ToString(),
                        Employee_Contact = selectedRow.Cells["Employee_Contact"].Value.ToString(),
                        Employee_Password = "",
                        Ride_ID = new foreignKey(Convert.ToInt32(selectedRow.Cells["Ride_ID"].Value.ToString()), "Rides", "Ride")
                    };
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), employee);
                }
                else
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), new Employee());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCustomersAddCustomersEvent(object sender, EventArgs e)
        {
            try
            {


                List<Control> controls = GenericFunctions.getInputs(this);

                Customer customer = GenericFunctions.CreateObjectFromControls<Customer>(controls.ToArray());
                MessageBox.Show(customer.ToString());
                string sql = "INSERT INTO Customers (Customer_Name, Customer_Surname, Customer_DOB, Customer_Contact, Customer_Email) " +
                                    "VALUES (@CustomerName, @CustomerSurname, @CustomerDOB, @CustomerContact, @CustomerEmail)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CustomerName", customer.Customer_Name),
                    new SqlParameter("@CustomerSurname", customer.Customer_Surname),
                    new SqlParameter("@CustomerDOB", customer.Customer_DOB),
                    new SqlParameter("@CustomerContact", customer.Customer_Contact),
                    new SqlParameter("@CustomerEmail", customer.Customer_Email)
                };
                DataBaseFuncitons.ChangeData(sql, parameters);

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

                MessageBox.Show("Chaning the following Customer: \n" + customer.ToString());
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int Customer_ID = Convert.ToInt32(selectedRow.Cells["Customer_ID"].Value.ToString());
                    string sql = "UPDATE Customers " + "SET Customer_Name = @Customer_Name, " +
                                             "Customer_Surname = @Customer_Surname, " +
                                          "    Customer_DOB = @Customer_DOB, " +
                                          "    Customer_Contact = @Customer_Contact, " +
                                          "    Customer_Email = @Customer_Email " +
                                          "WHERE Customer_ID = @Customer_ID";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@Customer_Name", customer.Customer_Name),
                    new SqlParameter("@Customer_Surname", customer.Customer_Surname),
                    new SqlParameter("@Customer_DOB", customer.Customer_DOB),
                    new SqlParameter("@Customer_Contact", customer.Customer_Contact),
                    new SqlParameter("@Customer_Email", customer.Customer_Email),
                    new SqlParameter("@Customer_ID", Customer_ID)
                    };
                    DataBaseFuncitons.ChangeData(sql, parameters);

                    BackClickedEvent(this, EventArgs.Empty);
                }
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

        private void MenuMaintainDeleteSelectedEmployeesEvent(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int ID = Convert.ToInt32(selectedRow.Cells["Employee_ID"].Value.ToString());
                    string name = selectedRow.Cells["Employee_Name"].Value.ToString();
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Employee " + name + "?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        string sql = "DELETE FROM Employees WHERE Employee_ID = @Employee_ID";

                        SqlParameter[] parameters = { new SqlParameter("@Employee_ID", ID) };
                        DataBaseFuncitons.ChangeData(sql, parameters);
                        sql = "SELECT Employees.Employee_ID, Employees.Employee_Name, Employee_Surname, Employee_Emergency_Contact, Employee_Contact, Rides.Ride_Name, Rides.Ride_ID FROM Employees JOIN Rides ON Employees.Ride_ID = Rides.Ride_ID";                       
                        parameters = new SqlParameter[0];
                        DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Employees");
                        dataGridView.Columns["Ride_ID"].Visible = false;
                    }
                }    
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
                
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    Customer customer = new Customer()
                    {
                        Customer_Name = selectedRow.Cells["Customer_Name"].Value.ToString(),
                        Customer_Surname = selectedRow.Cells["Customer_Surname"].Value.ToString(),
                        Customer_Contact = selectedRow.Cells["Customer_Contact"].Value.ToString(),
                        Customer_DOB = Convert.ToDateTime(selectedRow.Cells["Customer_DOB"].Value.ToString()),
                        Customer_Email = selectedRow.Cells["Customer_Email"].Value.ToString(),

                    };
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), customer);
                }
                else
                    GenericFunctions.PopulateControlsFromObject(controls.ToArray(), new Customer());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuMaintainDeleteSelectedCustomersEvent(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    int ID = Convert.ToInt32(selectedRow.Cells["Customer_ID"].Value.ToString());
                    string name = selectedRow.Cells["Customer_Name"].Value.ToString();
                    DialogResult result = MessageBox.Show("Are you sure you want to delete Customer " + name + "?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        string sql = "DELETE FROM Customers WHERE Customer_ID = @Customer_ID";

                        SqlParameter[] parameters = { new SqlParameter("@Customer_ID", ID) };
                        DataBaseFuncitons.ChangeData(sql, parameters);
                        sql = "Select * FROM Customers";
                        parameters = new SqlParameter[0];
                        DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Customers");

                    }
                }
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

        private void SellTicketsSellTicketsEvent(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = GenericFunctions.getInputs(this);
                ComboBox customer = (ComboBox)controls[0];
                ComboBox ride = (ComboBox)controls[1];
                NumericUpDown amount = new NumericUpDown();
                foreach (Control control in controls)
                {
                    if (control is NumericUpDown)
                    {
                        amount = (NumericUpDown)control;
                        break;
                    }
                }
                if (amount == new NumericUpDown())
                {
                    amount.Value = 0;
                }
                Transactions trans = new Transactions(new foreignKey(Convert.ToInt32(customer.SelectedValue),"Customers", "Customer"), new foreignKey(Convert.ToInt32(ride.SelectedValue), "Rides", "Ride"), Convert.ToInt32(amount.Value));
                MessageBox.Show("New Transaction: "+ trans.ToString());
                string sql = "INSERT INTO Transactions (Customer_ID, Ride_ID, Transaction_Date, Transaction_Time, Transaction_Amount) "+
                    "VALUES ((SELECT Customer_ID FROM Customers WHERE Customer_Name = @Customer_Name)," +
                    " (SELECT Ride_ID FROM Rides WHERE Ride_Name = @Ride_Name), @Transaction_Date, @Transaction_Time, @Transaction_Amount)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Customer_Name", trans.Customer_ID.name),
                    new SqlParameter("@Ride_Name", trans.Ride_ID.name),
                    new SqlParameter("@Transaction_Date", trans.Transaction_Date),
                    new SqlParameter("@Transaction_Time", trans.Transaction_Time),
                    new SqlParameter("@Transaction_Amount", trans.Transaction_Amount)
                };
                DataBaseFuncitons.ChangeData(sql, parameters);
                sql = "SELECT Transactions.Transaction_ID, Customers.Customer_Name AS Customer_Name, Rides.Ride_Name AS Ride_Name, Transactions.Transaction_Date, Transactions.Transaction_Time, Transactions.Transaction_Amount " +
                    "FROM Transactions " +
                    "INNER JOIN Customers ON Transactions.Customer_ID = Customers.Customer_ID " +
                    "INNER JOIN Rides ON Transactions.Ride_ID = Rides.Ride_ID";


                parameters = new SqlParameter[0];
                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                DataBaseFuncitons.DisplayData(sql, dataGridView, parameters, "Transactions");
                foreach (Control control in controls)
                {
                    if (control is NumericUpDown)
                    {
                        NumericUpDown num = (NumericUpDown)control;
                        num.Value = 1;
                    }
                    if (control is ComboBox)
                    {
                        ComboBox combo = (ComboBox)control;
                        combo.SelectedIndex = 0;
                    }
                }
                //BackClickedEvent(this, EventArgs.Empty); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuAllocateEmployeesEvent(object sender, EventArgs e)
        {
            NewMenuAllocateEmployees();
        }

        
        private void MainMenuReportsEvent(object sender, EventArgs e)
        {
                NewMenuRequestReports();
        }
        private void SelectionChangedOnTheDataGridViewRides(object sender, EventArgs e)
        {
            try
            {
                //GroupBox inputsGroupBox = Controls.OfType<GroupBox>().FirstOrDefault();
                List<Control> controls = GenericFunctions.getInputs(this);
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView != null)
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                        Rides ride = new Rides()
                        {
                            Ride_Name = selectedRow.Cells["Ride_Name"].Value.ToString(),
                            Ride_Description = selectedRow.Cells["Ride_Description"].Value.ToString(),
                            Ride_Availability = Convert.ToBoolean(selectedRow.Cells["Ride_Availability"].Value.ToString()),
                            Ride_Cost = Convert.ToInt32(selectedRow.Cells["Ride_Cost"].Value.ToString()),
                            Ride_Length = Convert.ToInt32(selectedRow.Cells["Ride_Length"].Value.ToString()),

                        };
                        GenericFunctions.PopulateControlsFromObject(controls.ToArray(), ride);
                    }

            }
            catch 
            { }
        }

        private void SelectionChangedOnTheDataGridViewEmployee(object sender, EventArgs e)
        {
            try
            {
                //GroupBox inputsGroupBox = Controls.OfType<GroupBox>().FirstOrDefault();
                List<Control> controls = GenericFunctions.getInputs(this);
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView != null)
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                        Employee employee = new Employee()
                        {
                            Employee_Name = selectedRow.Cells["Employee_Name"].Value.ToString(),
                            Employee_Surname = selectedRow.Cells["Employee_Surname"].Value.ToString(),
                            Employee_Emergency_Contact = selectedRow.Cells["Employee_Emergency_Contact"].Value.ToString(),
                            Employee_Contact = selectedRow.Cells["Employee_Contact"].Value.ToString(),
                            Employee_Password = "",
                            Ride_ID = new foreignKey()
                        };
                        GenericFunctions.PopulateControlsFromObject(controls.ToArray(), employee);
                    }

            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void SelectionChangedOnTheDataGridViewCustomer(object sender, EventArgs e)
        {
            try
            {
                //GroupBox inputsGroupBox = Controls.OfType<GroupBox>().FirstOrDefault();
                List<Control> controls = GenericFunctions.getInputs(this);
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView != null)
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                        Customer customer = new Customer()
                        {
                            Customer_Name = selectedRow.Cells["Customer_Name"].Value.ToString(),
                            Customer_Surname = selectedRow.Cells["Customer_Surname"].Value.ToString(),
                            Customer_DOB = Convert.ToDateTime(selectedRow.Cells["Customer_DOB"].Value.ToString()),
                            Customer_Email = selectedRow.Cells["Customer_Email"].Value.ToString(),
                            Customer_Contact = selectedRow.Cells["Customer_Contact"].Value.ToString()

                        };
                        GenericFunctions.PopulateControlsFromObject(controls.ToArray(), customer);
                    }

            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }
    }
}


