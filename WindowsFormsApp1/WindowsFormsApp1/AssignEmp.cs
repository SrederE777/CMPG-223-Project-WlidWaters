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
    public partial class AssignEmp : Form
    {
        public AssignEmp()
        {
            InitializeComponent();
        }

        private void AssignEmp_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AssignEmp_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm form = (mainForm)this.Owner;
            form.BackClickedEvent(this, EventArgs.Empty);
        }
    }
}
