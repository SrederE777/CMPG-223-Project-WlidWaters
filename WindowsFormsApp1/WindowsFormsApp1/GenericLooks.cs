using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{

    static class GenericLooks
    {
        private static Size inputsize = new Size(170, 20);
        static Dictionary<Type, Size> sizes = new Dictionary<Type, Size>
        {
            {typeof(Button), new Size(300, 100)},
            {typeof(GroupBox), new Size(300, 40)},
            {typeof(DataGridView),  new Size(1000, 500)},
            {typeof(NumericUpDown),inputsize},
            {typeof(TextBox), inputsize  },
            {typeof(ComboBox), inputsize },
            {typeof(DateTimePicker), inputsize },
        };

        public static Size GetSize(Type type)
        {
            if (sizes.ContainsKey(type))
            {
                return sizes[type];
            }
            else
            {
                throw new ArgumentException("Type not in GenericLooks Dictionary");
            }
        }

        public static void SetSize(Type type, Size size)
        {
            try
            {
                if (sizes.ContainsKey(type))
                {
                    sizes[type] = size;
                }
                else
                {
                    throw new ArgumentException("Type not in GenericLooks Dictionary");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ScaleControls(Control.ControlCollection controls, float scaleFactor)
        {
            try
            {
                foreach (Control control in controls)
                {
                    control.Scale(new SizeF(scaleFactor, scaleFactor));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetInputsLook(Control control)
        {

            try
            {
                if (sizes.ContainsKey(control.GetType()))
                {
                    control.Size = sizes[control.GetType()];
                }
                else
                {
                    throw new ArgumentException("Type not in GenericLooks Dictionary");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SetMenuLooks(Control control)
        {
            try
            {
                if (sizes.ContainsKey(control.GetType()))
                {
                    control.Size = sizes[control.GetType()];
                    if (control.GetType() == typeof(Button))
                    {
                        Button myButton = (Button)control;
                        // Set the button style to flat
                        myButton.FlatStyle = FlatStyle.Flat;

                        // Set the button background to transparent
                        myButton.BackColor = Color.Transparent;

                        // Remove the border around the button
                        myButton.FlatAppearance.BorderSize = 0;

                        // Set the color that will appear when you hover over the button
                        myButton.FlatAppearance.MouseOverBackColor = Color.Aquamarine;

                        // Set the color that will appear when you click on the button
                        myButton.FlatAppearance.MouseDownBackColor = Color.DarkRed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void ScaleControls(Control control, float scaleFactor)
        {
            ScaleControl(control, scaleFactor);

            // Recursively scale child controls
            foreach (Control child in control.Controls)
            {
                ScaleControls(child, scaleFactor);
            }
        }

        private static void ScaleControl(Control control, float scaleFactor)
        {
            control.Size = ScaleSize(control.Size, scaleFactor);
            control.Location = ScalePoint(control.Location, scaleFactor);
            control.Font = new Font(control.Font.FontFamily, control.Font.Size * scaleFactor);
        }

        private static Size ScaleSize(Size size, float scaleFactor)
        {
            return new Size((int)(size.Width * scaleFactor), (int)(size.Height * scaleFactor));
        }

        private static Point ScalePoint(Point point, float scaleFactor)
        {
            return new Point((int)(point.X * scaleFactor), (int)(point.Y * scaleFactor));
        }

    }
}



            
        
    