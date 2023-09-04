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
            if (sizes.ContainsKey(type))
            {
                sizes[type] = size;
            }
            else
            {
                throw new ArgumentException("Type not in GenericLooks Dictionary");
            }
        }

        public static void ScaleControls(Control.ControlCollection controls, float scaleFactor)
        {
            foreach (Control control in controls)
            {
                control.Scale(new SizeF(scaleFactor, scaleFactor));
            }
        }

        public static void SetInputsLook(Control control)
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

        public static void SetMenuLooks(Control control)
        {
            if (sizes.ContainsKey(control.GetType()))
            {
                control.Size = sizes[control.GetType()];
            }
            
        }


    }
}
