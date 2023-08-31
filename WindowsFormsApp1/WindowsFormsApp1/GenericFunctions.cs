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

        public static void CreateInputs<T>(GroupBox outputOn)
        {
            Size groupBoxSize = outputOn.Size;
            //foreach (T item in input)
            {

                // Get the properties of the class
                FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance); 
                int amount = fields.Length;
                int inc = 1;
                // Loop through the properties
                foreach (FieldInfo field in fields)
                {
                    int spacing = (groupBoxSize.Height - 50) / (amount + 1); ;
                    // Get the value of the property
                    //object value = property.GetValue(item);

                    // Perform different actions based on the type of the property
                    if (field.FieldType == typeof(int))
                    {
                        
                        TextBox myText = new TextBox();
                        myText.Location = new Point(25, spacing * inc);
                        outputOn.Controls.Add(myText);
                        inc++;
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        TextBox myText = new TextBox();
                        myText.Location = new Point(25, spacing * inc);
                        outputOn.Controls.Add(myText);
                        inc++;
                    }
                    else if (field.FieldType == typeof(DateTime))
                    {
                        TextBox myText = new TextBox();
                        myText.Location = new Point(25, spacing * inc);
                        outputOn.Controls.Add(myText);
                        inc++;
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        TextBox myText = new TextBox();
                        myText.Location = new Point(25, spacing * inc);
                        outputOn.Controls.Add(myText);
                        inc++;
                    }
                    else if (field.FieldType == typeof(double))
                    {
                        TextBox myText = new TextBox();
                        myText.Location = new Point(25, spacing * inc);
                        outputOn.Controls.Add(myText);
                        inc++;
                    }

                }
            }
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
