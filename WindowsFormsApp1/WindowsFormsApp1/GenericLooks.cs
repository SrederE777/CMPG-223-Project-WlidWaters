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
        public static void SetInputsLook(Control control)
        {
            control.Size = new Size(170, 50);
        }

        public static void SetMenuLooks(Control control)
        {
            control.Size = new Size(170, 50);
        }
    }
}
