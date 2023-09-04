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
            
        };

        public class ControlTypeAttribute : Attribute
        {
            // Define a property that stores the type of control
            public Type ControlType { get; set; }

            // Define a constructor that takes the type of control as a parameter
            public ControlTypeAttribute(Type controlType)
            {
                // Assign the parameter to the property
                ControlType = controlType;
            }
        }

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
                            value = numericUpDown.Value;
                        }
                        else if (control is ComboBox comboBox)
                        {
                            value = comboBox.SelectedItem;
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


        //This may not be needed.... Which would suck a bit but hey if it works right
        public static void CreateForm<T>(String title, MDIParent parentForm) where T : Form, new()
        {
            //Create an instance of the form
            T form = new T();
            //This was used instead of new beacause it is more generic and allows for multiple form to be made according to a preset

            //set the configuration for the forms made with this method
            form.MdiParent = parentForm;
            form.WindowState = FormWindowState.Maximized;
            form.ControlBox = false;
            form.Show();
            form.Text = title;
        }


        public static void CreateDisplay(Type dataType, Point location, Control container, string text)
        {

            Control control = (Control)Activator.CreateInstance(dataType);
            control.Location = location;
            container.Controls.Add(control);
            control.Text = text;
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


        public static List<Control> CreateInputs<T>(Control outputOn, int widthMargin, int heightMargin) where T : class
        {
            
            //foreach (T item in input)
            {

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
                    fields.Count();
                    
                    
                    int spacingHight = (heightMargin + GenericLooks.GetSize(types[field.FieldType]).Height);
                    outputOn.Size = new Size(outputOn.Size.Width, spacingHight + outputOn.Size.Height);
                    Size groupBoxSize = outputOn.Size;
                    int spacingWidth = (groupBoxSize.Width - widthMargin) / 4;
                    Point location = new Point(spacingWidth, spacingHight * inc);

                    //if (types.ContainsKey(types[field.FieldType]))
                    {
                        Type controlType = types[field.FieldType];
                        returnControls.Add(CreateInput(controlType, location, outputOn));
                        
                        location.X /= 8;
                        
                        controlType = typeof(Label);
                        CreateDisplay(controlType, location, outputOn, "Label " + inc);
                        
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
            //MessageBox.Show(control.Size.ToString());
            return control;
            
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
