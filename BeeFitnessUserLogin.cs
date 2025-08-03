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
    public partial class BeeFitnessUserLogin : Form
    {
        int uid;
        public BeeFitnessUserLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserClass user = new UserClass();
            
            //input validation code

            user.Uname = txtUserName.Text;  
            user.Upassword = txtPassword.Text;

            uid=user.Login();
            if (uid != 0)
            {
                MessageBox.Show("Login success");
                btnUpdate.Enabled = true;
                btnFitness.Enabled = true;

            }
            else
            {
                MessageBox.Show("Login Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            BeeFitnessUpdateUser frmUpdateUser = new BeeFitnessUpdateUser(uid);
            frmUpdateUser.ShowDialog();
        }

        private void btnFitness_Click(object sender, EventArgs e)
        {
            BeeFitnessFitnessActivities frm   =new BeeFitnessFitnessActivities(uid);
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
