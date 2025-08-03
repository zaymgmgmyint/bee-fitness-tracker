using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BeeFitnessTracker
{

    internal class FitnessClass
    {
        //for database command
        OleDbCommand cmd;
        OleDbConnection con;
        string conStr;

        //for metrics and calories
        private double m1, m2, m3, calorie;

        public FitnessClass()
        {
            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=" + Application.StartupPath + @"\Database.mdb";

            con = new OleDbConnection(); //create connection object
            con.ConnectionString = conStr;
            con.Open();
            cmd = new OleDbCommand();
            cmd.Connection = con;
        }
        public double M1 { get { return m1; } set { m1 = value; } }
        public double M2 { get { return m2; } set { m2 = value; } }
        public double M3 { get { return m3; } set { m3 = value; } }
        public double Calorie { get { return calorie; } set { calorie = value; } }

        public bool InsertMetric(int uid, int aid, float cal, DateTime activityDate)
        {
            try
            {
                cmd.Parameters.Clear();
                string commandStr = @"
                INSERT INTO [tblMetric] ([userId], [aid], [Calorie], [aDate]) 
                VALUES (?, ?, ?, ?)";
                 
                cmd.Parameters.AddWithValue("?", uid);
                cmd.Parameters.AddWithValue("?", aid);
                cmd.Parameters.AddWithValue("?", cal);
                cmd.Parameters.AddWithValue("?", activityDate); // DateTime is handled safely

                cmd.CommandText = commandStr;
                cmd.ExecuteNonQuery();
                con.Close();
                    
                return true;
            }
            catch (OleDbException ex)
            {
                // Optional: log ex.Message or rethrow for debugging
                string msg = ex.Message;
                return false;
            }

        }
        public double GetGoalCalorie(int uid)
        {
            double goalCalorie = 0;
            string query = "SELECT [goalCalorie] FROM [tblUser] WHERE [userId] = ?";

            using (OleDbConnection con = new OleDbConnection(conStr))
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                cmd.Parameters.AddWithValue("?", uid);

                try
                {
                    con.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            goalCalorie = Convert.ToDouble(reader[0]); // Safer for numeric types
                        }
                    }
                }
                catch (OleDbException)
                {
                    // Consider logging the error or rethrowing
                    
                }
            }

            return goalCalorie;

        }
        public double GetTotalCalorie(int uid)
        {
            double totalCalorie = 0;
            string query = "SELECT SUM([Calorie]) FROM [tblMetric] WHERE [userId] = ?";
            con.Open();
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("?", uid);
                  cmd.CommandText= query;
                  OleDbDataReader reader = cmd.ExecuteReader();
                  if (reader.Read() && !reader.IsDBNull(0))
                        {
                            totalCalorie = reader.GetDouble(0);
                        }
              
            }
            catch (OleDbException oex)
            {
                string msg = oex.Message;
            }
            
             con.Close();
            

            return totalCalorie;
        }

        public DataTable ShowUserActvities(int uid)
        {
            DataTable dt = new DataTable();

            con = new OleDbConnection();
            con.ConnectionString = conStr;
            con.Open();

            cmd = new OleDbCommand();
            cmd.Connection = con;

            string qryStr = "SELECT * FROM [tblMetric] WHERE userId=" + uid;
            cmd.CommandText = qryStr;

            OleDbDataAdapter dataAda = new OleDbDataAdapter(cmd);
            dataAda.Fill(dt);

            con.Close();
            return dt;
        }


    }
}
