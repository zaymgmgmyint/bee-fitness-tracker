using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data; //for application path

namespace BeeFitnessTracker
{
    internal class UserClass
    {
        private string uid;
        private string uname;
        private string upassword;
        private double goalCal;

        //for database command
        OleDbCommand cmd;
        OleDbConnection con;
        string conStr;

        public UserClass()
        {
            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
             "Data Source=" + Application.StartupPath + @"\Database.mdb";

            con = new OleDbConnection(); //create connection object
            con.ConnectionString = conStr;
            con.Open();
            cmd = new OleDbCommand();
            cmd.Connection = con;
        }

        public string Uid
        { get { return uid; } set { uid = value; } }
        public string Uname
        {
            get { return uname; }
            set { uname = value; }
        }
        public string Upassword
        {
            get { return upassword; }       
            set { upassword = value; }
        }
        public double GoalCal {  get { return goalCal; } set { goalCal = value; } }

        public int CheckExisitingUser(string userName)
        {
            int id = 0;

            string query = "SELECT userId FROM [tblUser] WHERE userName = ?";
            cmd.CommandText = query;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", userName);

            OleDbDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                if (reader != null && reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        id = reader.GetInt32(0);
                }
            }
            catch (OleDbException oex)
            {
                // Log or handle exception if needed
                string msg = oex.Message;
            }
            finally
            {
                reader.Close();
                con.Close();
            }

            return id;

        }
        public int Registration()
        {
            int id = 0;

            try
            {
                      
                    cmd.CommandText = "INSERT INTO [tblUser] ([userName], [password], [goalCalorie]) VALUES (?, ?, ?)";
                    cmd.Parameters.AddWithValue("?", this.uname);
                    cmd.Parameters.AddWithValue("?", this.upassword);
                    cmd.Parameters.AddWithValue("?", this.goalCal);
                    cmd.ExecuteNonQuery(); // Proper method for INSERT
                

                // After successful insert, check for the new user's ID
                id = CheckExisitingUser(this.uname);
            }
            catch (OleDbException oex)
            {
                // Log or handle the error as needed
                string msg = oex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return id;

        }
        public int Login()
        {
            int id = 0;

              try
              {
                    cmd.CommandText = @"
                    SELECT userId 
                    FROM [tblUser] 
                    WHERE StrComp(userName, ?, 0) = 0 
                    AND StrComp(password, ?, 0) = 0"; // 0 = binary (case-sensitive)

                    cmd.Parameters.AddWithValue("?", this.uname);
                    cmd.Parameters.AddWithValue("?", this.upassword);


                     OleDbDataReader reader = cmd.ExecuteReader();
                    
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            id = reader.GetInt32(0);
                        }
                    
                
              }
               catch (OleDbException oex)
              {
                // Optional: log or handle the error
                string msg = oex.Message;
              }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return id;

        }
        public bool UpdateUser(int uid, string password, double calorie)
        {
            string query = @"
            UPDATE [tblUser] 
            SET [password] = ?, [goalCalorie] = ?
            WHERE [userId] = ?";

            try
            {
                cmd.CommandText = query;
                cmd.Parameters.Clear(); // Clear previous parameters if reused
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("?", password);
                cmd.Parameters.AddWithValue("?", calorie);
                cmd.Parameters.AddWithValue("?", uid);

                
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (OleDbException oex)
            {
                // Optional: log error message
                string msg = oex.Message;
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }



    }
}
