using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TestForm_CanBeDeleted_ : Form
    {
        public TestForm_CanBeDeleted_()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TestForm_CanBeDeleted__Load(object sender, EventArgs e)
        {
            List<string> test = new List<string>
            {
                "Button1",
                "Button2",
                "Button3"
            };
            
            GenericFunctions.CreateMenu(test, this, new Point(groupBox2.Location.X + groupBox2.Size.Width + 10, groupBox2.Location.Y + 5));
            GenericFunctions.CreateInputs<Rides>(groupBox1, -100, 100);
            GenericFunctions.CreateInputs<Employee>(groupBox2, -100, 100);
            
            
        }
    }
}
