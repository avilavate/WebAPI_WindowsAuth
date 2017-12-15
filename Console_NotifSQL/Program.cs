using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_NotifSQL
{
    class Program
    {
        public int initial { get; set; }
        static void Main(string[] args)
        {
            SqlDependency.Start(@"Data Source=BEK-47674357\sqlexpress;Initial Catalog=NotificationList;Integrated Security=True");
            Program p = new Program();
            p.GetMessages();
            //p.Initialization();
            ////Before:
            //SqlConnection conn = new SqlConnection(@"Data Source=BEK-47674357\sqlexpress;Initial Catalog=NotificationList;Integrated Security=True");
            //// Create a new SqlCommand object.  
            //conn.Open();
            //using (SqlCommand command = new SqlCommand(
            //   "select COUNT(*) from NotificationList"
            //    , conn))
            //{
            //    p.initial = command.ExecuteNonQuery();

            //}


            //p.SomeMethod();
            Console.WriteLine("Waiting for SQL tab;e updates..");
            Console.Read();
        }
        private void GetNames()
        {
            if (!DoesUserHavePermission())
                return;

            lbNames.Items.Clear();

            //  You must stop the dependency before starting a new one.
            //  You must start the dependency when creating a new one.
            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT FirstName, LastName FROM dbo.[Users]";

                    cmd.Notification = null;

                    //  creates a new dependency for the SqlCommand
                    SqlDependency dep = new SqlDependency(cmd);
                    //  creates an event handler for the notification of data
                    //      changes in the database.
                    //  NOTE: the following code uses the normal .Net capitalization methods, though
                    //      the forum software seems to change it to lowercase letters
                    dep.onchange += new onchangeEventHandler(dep_onchange);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lbNames.Items.Add(dr.GetString(0) + " " + dr.GetString(1));
                        }
                    }
                }
            }
        }

        private bool DoesUserHavePermission()
        {
            try
            {
                SqlClientPermission clientPermission = new SqlClientPermission(PermissionState.Unrestricted);

                // will throw an error if user does not have permissions
                clientPermission.Demand();

                return true;
            }
            catch
            {
                return false;
            }
        }
        void dep_onchange(object sender, SqlNotificationEventArgs e)
        {
            // this event is run asynchronously so you will need to invoke to run on UI thread(if required).
            if (this.InvokeRequired)
                lbNames.BeginInvoke(new MethodInvoker(GetNames));
            else
                GetNames();

            // this will remove the event handler since the dependency is only for a single notification
            SqlDependency dep = sender as SqlDependency;

            //  NOTE: the following code uses the normal .Net capitalization methods, though
            //      the forum software seems to change it to lowercase letters
            dep.onchange -= new onchangeEventHandler(dep_onchange);
        }

    }
}
