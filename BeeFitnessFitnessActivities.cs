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
    public partial class BeeFitnessFitnessActivities : Form
    {
        int userId;
        int aid; //activity id 
        
        public BeeFitnessFitnessActivities(int uid)
        {
            InitializeComponent();
            this.userId = uid;
        }

        private void frmFitnessActivities_Load(object sender, EventArgs e)
        {
            lblUserId.Text = "Welcome user=" + userId.ToString();
            
            FitnessClass fc = new FitnessClass();
            lblGoal.Text = "Goal Calorie=" +fc.GetGoalCalorie(this.userId).ToString();
        }

        private void rdoWalking_CheckedChanged(object sender, EventArgs e)
        {
            aid = 1;
            var choices = new Dictionary<string, string>();
            choices["2.8"] = "Light= 2.8";
            choices["3.5"] = "Moderate = 3.5";
            choices["5.0"] = "Fast=5.0";
            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";
        }

        private void rdoSwimming_CheckedChanged(object sender, EventArgs e)
        {
            aid = 2;
            var choices = new Dictionary<string, string>();
            choices["6.0"] = "Leisurely swimming = 6.0";
            choices["5.8"] = "moderate effort =5.8";
            choices["9.8"] = "freestyle, fast=9.8";
            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";
        }

        private void rdoYoga_CheckedChanged(object sender, EventArgs e)
        {
            aid = 3; // Yoga activity id
            var choices = new Dictionary<string, string>();
            choices["2.5"] = "Light Yoga (e.g., Hatha) = 2.5";
            choices["4.0"] = "Moderate Yoga (e.g., Vinyasa) = 4.0";
            choices["5.5"] = "Intense Yoga (e.g., Hot Yoga) = 5.5";

            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";

        }

        private void rdoRunning_CheckedChanged(object sender, EventArgs e)
        {
            aid = 4; // Running activity id
            var choices = new Dictionary<string, string>();

            choices["7.0"] = "Jogging (slow pace ~6.4 km/h) = 7.0";
            choices["8.3"] = "Running (moderate pace ~8 km/h) = 8.3";
            choices["11.0"] = "Running (fast pace ~10.8 km/h) = 11.0";

            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";

        }

        private void rdoCycling_CheckedChanged(object sender, EventArgs e)
        {
            aid = 5; // Cycling activity id
            var choices = new Dictionary<string, string>();

            choices["4.0"] = "Leisure Cycling (<16 km/h) = 4.0";
            choices["6.8"] = "Moderate Cycling (16–19 km/h) = 6.8";
            choices["8.0"] = "Vigorous Cycling (19–22 km/h) = 8.0";

            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";

        }

        private void rdoHousehold_CheckedChanged(object sender, EventArgs e)
        {
            aid = 6; // Household activity id
            var choices = new Dictionary<string, string>();

            choices["2.5"] = "Light Cleaning (e.g., dusting) = 2.5";
            choices["3.5"] = "Moderate Cleaning (e.g., sweeping, mopping) = 3.5";
            choices["4.5"] = "Heavy Cleaning (e.g., scrubbing floors) = 4.5";

            cboMetric3.DataSource = new BindingSource(choices, null);
            cboMetric3.DisplayMember = "Value";
            cboMetric3.ValueMember = "Key";

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //txtMetric1 is time
            float time = float.Parse(txtMetric1.Text);

            //txtMetric2 is weight
            float weight=float.Parse(txtMetric2.Text);

            //cboMetric3 is MET
            float MET = float.Parse(cboMetric3.SelectedValue.ToString());

            //calculate calorie
            // float toDaycalorie = time * weight * MET;
            float toDaycalorie = (MET * 3.5f * weight / 200) * time;
            lblTodayCalorie.Text = "Today calorie burnt=" + toDaycalorie.ToString();
            
            FitnessClass fc = new FitnessClass();
            fc.InsertMetric(this.userId, aid, toDaycalorie, DateTime.Now);

            double totalCalorie;
            totalCalorie =fc.GetTotalCalorie(this.userId);
            lblTotalCalorie.Text = "Total Calorie=" + totalCalorie.ToString();

            double goalCalorie;
            goalCalorie = fc.GetGoalCalorie(this.userId);
            
            double remain= goalCalorie - totalCalorie;
            if (remain <= 0) lblStatus.Text = "Your goal reached!";
            else lblStatus.Text = remain.ToString() + " remaining";


        }
    }
}
