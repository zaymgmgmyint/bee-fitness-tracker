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
    public partial class BeeFitnessUpdateUser : Form
    {
        int uid;
        public BeeFitnessUpdateUser(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            lblUserID.Text = uid.ToString();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            UserClass user = new UserClass();
           bool status= user.UpdateUser(uid, txtPassword.Text, double.Parse(txtGoalCalorie.Text));
           if(status)
            {
                MessageBox.Show("Updated");
            }
            else
            {
                MessageBox.Show("Update Error");
            }
        
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
