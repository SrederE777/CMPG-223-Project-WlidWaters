using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    static class GenericFunctions
    {
        static Dictionary<Type, Type> types = new Dictionary<Type, Type>
        {
            { typeof(int), typeof(NumericUpDown) },
            { typeof(string), typeof(TextBox) },
            { typeof(bool), typeof(ComboBox)},
            { typeof(DateTime), typeof(DateTimePicker)},
            { typeof(double), typeof(NumericUpDown)},
            {typeof(decimal), typeof(NumericUpDown) },
            {typeof(Rides), typeof(ComboBox) },
            {typeof(Customer), typeof(ComboBox) },
            {typeof(Employee), typeof(ComboBox) },
            {typeof(Transactions), typeof(ComboBox)},
            {typeof(foreignKey), typeof(ComboBox)}

        };

        // Define a generic method that takes a type parameter T and an array of controls as parameters
        public static T CreateObjectFromControls<T>(Control[] controls) where T : new()
        {
            // Create an instance of type T using the default constructor
            T obj = new T();

            // Get the type information of type T using reflection
            Type type = typeof(T);

            // Get the properties or fields of type T using reflection
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);
            int j = 0;
            // Loop through the properties or fields
            for (int i = 0; i < members.Length; i++)
            {
                // Get the current property or field
                var member = members[i];

                // Get the type of the property or field using reflection
                Type memberType;
                if (member is PropertyInfo propertyInfo)
                {
                    memberType = propertyInfo.PropertyType;
                }
                else if (member is FieldInfo fieldInfo)
                {
                    memberType = fieldInfo.FieldType;
                }
                else
                {
                    // If it is not a property or field, then skip it
                    continue;
                }
                // Check if the dictionary contains a mapping for the type of the property or field
                if (types.ContainsKey(memberType))
                {
                    // Get the type of control that corresponds to the type of the property or field from the dictionary
                    Type controlType = types[memberType];
                    // Get the corresponding control from the array of controls using the index i
                    Control control = controls[j];
                    j++;
                    // Check if the control is of the same type as specified by the dictionary
                    if (control.GetType() == controlType)
                    {
                        // Get the value from the control using different properties or methods depending on the type of control
                        object value;
                        if (control is TextBox textBox)
                        {
                            value = textBox.Text;
                        }
                        else if (control is NumericUpDown numericUpDown)
                        {
                            if (memberType == typeof(double))
                            {
                                value = (double)numericUpDown.Value;
                            }
                            else if(memberType == typeof(int))
                            {
                                value = (int)numericUpDown.Value;
                            }
                            else
                            {
                                value = numericUpDown.Value;
                            }
                        }
                        else if (control is ComboBox comboBox)
                        {
                            if (bool.TryParse(comboBox.SelectedItem.ToString(), out bool parsedValue))
                            {
                                value = parsedValue;
                            }
                            else
                            {
                                value = new foreignKey(Convert.ToInt32(comboBox.SelectedValue.ToString()));
                                //string sql = "SELECT * FROM Rides WHERE Ride_ID = @Ride_ID";
                                //SqlParameter[] parameters = new SqlParameter[]
                                {
                                     //new SqlParameter("@Ride_ID", IDValue)
                                };
                                //value = DataBaseFuncitons.GetData<Rides>(sql, parameters);
                            }
                        }
                        else if (control is DateTimePicker dateTimePicker)
                        {
                            value = dateTimePicker.Value;
                        }
                        else
                        {
                            // If none of these types match, then use null as a default value
                            value = null;
                        }
                        // Assign the value to the property or field using reflection
                        if (member is PropertyInfo propertyInfoes)
                        {
                            propertyInfoes.SetValue(obj, value);
                        }
                        else if (member is FieldInfo fieldInfo)
                        {
                            fieldInfo.SetValue(obj, value);
                        }
                    }
                }
            }

            // Return the object of type T
            return obj;
        }

        public static void PopulateControlsFromObject<T>(Control[] controls, T obj)
        {
            // Get the type information of type T using reflection
            Type type = typeof(T);

            // Get the properties or fields of type T using reflection
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);
            int j = 0;
            // Loop through the properties or fields
            for (int i = 0; i < members.Length; i++)
            {
                // Get the current property or field
                var member = members[i];

                // Get the type of the property or field using reflection
                Type memberType;
                object value;
                if (member is PropertyInfo propertyInfo)
                {
                    memberType = propertyInfo.PropertyType;
                    value = propertyInfo.GetValue(obj);
                }
                else if (member is FieldInfo fieldInfo)
                {
                    memberType = fieldInfo.FieldType;
                    value = fieldInfo.GetValue(obj);
                }
                else
                {
                    // If it is not a property or field, then skip it
                    continue;
                }

                // Check if the dictionary contains a mapping for the type of the property or field
                if (types.ContainsKey(memberType))
                {
                    // Get the type of control that corresponds to the type of the property or field from the dictionary
                    Type controlType = types[memberType];

                    // Get the corresponding control from the array of controls using the index i
                    Control control = controls[j];
                    j++;
                    // Check if the control is of the same type as specified by the dictionary
                    if (control.GetType() == controlType)
                    {
                        // Set the value of the control using different properties or methods depending on the type of control
                        if (control is TextBox textBox)
                        {
                            textBox.Text = value.ToString();
                        }
                        else if (control is NumericUpDown numericUpDown)
                        {
                            numericUpDown.Value = Convert.ToDecimal(value);
                        }
                        else if (control is ComboBox comboBox)
                        {
                            comboBox.SelectedItem = value;
                            

                        }
                        else if (control is DateTimePicker dateTimePicker)
                        {
                            dateTimePicker.Value = Convert.ToDateTime(value);
                        }
                    }
                }
            }
        }



        //This may not be needed.... Which would suck a bit but hey if it works right
        public static void CreateForm<T>(String title, Form parentForm) where T : Form, new()
        {
            try
            {
                //Create an instance of the form
                T form = new T();
                //This was used instead of new beacause it is more generic and allows for multiple form to be made according to a preset

                //set the configuration for the forms made with this method
                
                form.WindowState = FormWindowState.Maximized;
                form.ControlBox = false;
                form.Text = title;
                form.ShowDialog(parentForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static void CreateDisplay(Type dataType, Point location, Control container, string text)
        {
            try
            {
                 Control control = (Control)Activator.CreateInstance(dataType);
            control.Location = location;
            container.Controls.Add(control);
            control.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Control CreateMenuItem(Type dataType, Point location, Control container, string text)
        {
            Control control = (Control)Activator.CreateInstance(dataType);
            control.Location = location;
            container.Controls.Add(control);
            GenericLooks.SetMenuLooks(control);
            control.Text = text;
            control.Name = text.Replace(" ", "");
            return control;
        }
        public static List<Control> CreateMenu(Dictionary<string,Type> options, ContainerControl outputOn, Point locaiton, int SpacingBetweenButtons = 0)
        {
            int inc = 0;
            List<Control> controls = new List<Control>();
            foreach (string option in options.Keys)
            {
                Type currentType = options[option];

                controls.Add(CreateMenuItem(currentType, new Point(locaiton.X, locaiton.Y + inc), outputOn, option));
                inc += GenericLooks.GetSize(currentType).Height + SpacingBetweenButtons;

 
                
            }
            return controls;
        }

        public static List<Control> CreateMenu(List<string> options, ContainerControl outputOn, Point locaiton, int SpacingBetweenButtons = 0)
        {
            int inc = 0;
            List<Control> controls = new List<Control>();
            foreach (string option in options)
            {
                Type currentType = typeof(Button);

                controls.Add(CreateMenuItem(currentType, new Point(locaiton.X, locaiton.Y + inc), outputOn, option));
                inc += GenericLooks.GetSize(currentType).Height + SpacingBetweenButtons;



            }
            return controls;
        }


        public static List<Control> CreateInputs<T>(Control outputOn, int widthMargin, int heightMargin) where T : class, DataClasses
        {
            
            //foreach (T item in input)
            {
                T instance = Activator.CreateInstance<T>();

                // Call the getName method on the instance
                List<string> names = instance.getName();
                // Get the properties of the class
                List<Control> returnControls = new List<Control>();
                List<FieldInfo> fields = new List<FieldInfo>();
                Type type = typeof(T);
                while (type != null)
                {
                    fields.AddRange(type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance));
                    type = type.BaseType;
                }
         
                int inc = 1;
                
                // Loop through the properties
                foreach (FieldInfo field in fields)
                {
                    //fields.Count();
                    int spacingHight = (heightMargin + GenericLooks.GetSize(types[field.FieldType]).Height);
                    outputOn.Size = new Size(outputOn.Size.Width, spacingHight + outputOn.Size.Height);
                    Size groupBoxSize = outputOn.Size;
                    int spacingWidth = (groupBoxSize.Width - widthMargin) / 4;
                    Point location = new Point(spacingWidth, spacingHight * inc);
                    {
                        Type controlType = types[field.FieldType];
                        Control control = CreateInput(controlType, location, outputOn);
                        control.Name = field.Name; // Set the name of the control
                        returnControls.Add(control);

                        location.X /= 16;
                        
                        controlType = typeof(Label);
                        CreateDisplay(controlType, location, outputOn, names[inc-1]);
                        
                        inc++;
                    }
                    
                    

                }
                return returnControls;
            }
        }
        public static Control CreateInput(Type dataType, Point location, Control container)
        {
            Control control = (Control)Activator.CreateInstance(dataType);
            control.Location = location;
            container.Controls.Add(control);
            GenericLooks.SetInputsLook(control);
            if (dataType == typeof(ComboBox))
            {
                string[] items = new string[] { "True", "False" };
                ComboBox comboBox = (ComboBox)control;
                comboBox.Items.AddRange(items);
            }

            //MessageBox.Show(control.Size.ToString());
            return control;
            
        }

        public static List<Control> getInputs(Form parent)
        {
            GroupBox inputs = new GroupBox();
            foreach (Control c in parent.Controls)
            {
                if (c is GroupBox)
                {
                    inputs = (GroupBox)c;
                }
            }

            List<Control> controls = new List<Control>();
            foreach (Control c in inputs.Controls)
            {
                if (c is TextBox)
                {
                    controls.Add((TextBox)c);
                }
                else if (c is NumericUpDown)
                {
                    controls.Add((NumericUpDown)c);
                }
                else if (c is ComboBox)
                {
                    controls.Add((ComboBox)c);
                }
                else if (c is DateTimePicker)
                {
                    controls.Add((DateTimePicker)c);
                }
            }
            return controls;
        }

        
        public static List<T> Fill<T>(this SqlDataReader reader) where T : new()
        {

            List<T> res = new List<T>();
            while (reader.Read())
            {
                T t = new T();
                for (int inc = 0; inc < reader.FieldCount; inc++)
                {
                    Type type = t.GetType();
                    string name = reader.GetName(inc);
                    PropertyInfo prop = type.GetProperty(name);
                    if (prop != null)
                    {
                        if (name == prop.Name)
                        {
                            var value = reader.GetValue(inc);
                            if (value != DBNull.Value)
                            {
                                prop.SetValue(t, Convert.ChangeType(value, prop.PropertyType), null);
                            }
                            prop.SetValue(t, value, null);

                        }
                    }
                }
                res.Add(t);
            }
            reader.Close();

            return res;
        }

    }

}
