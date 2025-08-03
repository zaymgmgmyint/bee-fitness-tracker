using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeFitnessTracker
{
    public partial class BeeFitnessMain : Form
    {
        public BeeFitnessMain()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            BeeFitnessRegisterUser reg=new BeeFitnessRegisterUser();
            reg.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            BeeFitnessUserLogin login = new BeeFitnessUserLogin();
            login.ShowDialog();
        }
    }
}
