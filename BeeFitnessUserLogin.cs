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

            // Presence check for user name
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("User name can't be empty!");
                txtUserName.Focus();
                return;
            }

            // Presence check for password
            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Password can't be empty!");
                txtPassword.Focus();
                return;
            }

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
                // Execuet the user name and password validation logic
               MessageBox.Show("Login failed. Please check your user name and password.");
                txtUserName.Focus();
                btnUpdate.Enabled = false;
                btnFitness.Enabled = false;
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

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; //show password
            }
            else
            {
                txtPassword.PasswordChar = '*'; //hide password
            }
        }
    }
}
