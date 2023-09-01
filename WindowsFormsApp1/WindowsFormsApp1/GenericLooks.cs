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
        static Dictionary<Type, Size> sizes = new Dictionary<Type, Size>
        {
            {typeof(Button), new Size(300, 100)}
        };
        public static void SetInputsLook(Control control)
        {
            control.Size = new Size(170, 50);
        }

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

        public static void ScaleControls(List<Control> controls, float scaleFactor)
        {
            foreach (Control control in controls)
            {
                control.Scale(new SizeF(scaleFactor, scaleFactor));
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
