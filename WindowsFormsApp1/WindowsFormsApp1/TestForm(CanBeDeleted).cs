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
            GenericFunctions.CreateInputs<Employee>(groupBox1);
        }
    }
}
