using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeFitnessTracker
{
    public partial class BeeFitnessRegisterUser : Form
    {
        public BeeFitnessRegisterUser()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            UserClass user=new UserClass();

            //  Presence check for user name
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user name");
                txtUserName.Focus();
                return;
            }
            else
            {
                // User name must be character and numeric and 5-10 characters long
                Regex regex = new Regex(@"^[a-zA-Z0-9]{5,10}$");    
                if (!regex.IsMatch(txtUserName.Text.Trim()))
                {
                    MessageBox.Show("User name must be alphanumeric and 5-10 characters long");
                    txtUserName.Focus();
                    return;
                }

                // Check if user already exists
                int existingUserId = user.CheckExisitingUser(txtUserName.Text.Trim());
                if (existingUserId != 0)
                {
                    MessageBox.Show("User already exists with user id=" + existingUserId.ToString());
                    txtUserName.Focus();
                    return;
                }

            }

            //  Presence check for password
            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter password");
                txtPassword.Focus();
                return;
            }
            else
            {
                // Pasword must be character and numeric and 6-10 characters long and at least one special character
                Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,10}$");
                if (!regex.IsMatch(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Password must be alphanumeric, 6-10 characters long and at least one special character");
                    txtPassword.Focus();
                    return;
                }
            }

            //  Presence check for goal calorie
            if (txtGoalCalorie.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter goal calorie");
                txtGoalCalorie.Focus();
                return;
            }

            user.Uname =txtUserName.Text;
            user.Upassword =txtPassword.Text;
            user.GoalCal =double.Parse(txtGoalCalorie.Text);

            int uid= user.Registration();
            if (uid != 0)
            {
                MessageBox.Show("Registration success with userid=" + uid.ToString());
            }
            else
                MessageBox.Show("Registration Error");
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
