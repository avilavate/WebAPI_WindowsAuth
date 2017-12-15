using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColorApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Initialization();
            SomeMethod();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        void Initialization()
        {
            // Create a dependency connection.  
            SqlDependency.Start(@"Data Source=BEK-47674357\sqlexpress;Initial Catalog=NotificationList;Integrated Security=True");
        }

        void SomeMethod()
        {
            // Assume connection is an open SqlConnection.  
            SqlConnection conn = new SqlConnection(@"Data Source=BEK-47674357\sqlexpress;Initial Catalog=NotificationList;Integrated Security=True");
            // Create a new SqlCommand object.  
            conn.Open();
            using (SqlCommand command = new SqlCommand(
               "select * from notificationList"
                ,conn))
            {

                // Create a dependency and associate it with the SqlCommand.  
                SqlDependency dependency = new SqlDependency(command);
                // Maintain the refence in a class member.  

                // Subscribe to the SqlDependency event.  
                dependency.OnChange += new
                   OnChangeEventHandler(OnDependencyChange);

                // Execute the command.  
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Process the DataReader.  
                }
            }
        }

        // Handler method  
        void OnDependencyChange(object sender,
           SqlNotificationEventArgs e)
        {
            Contact();
        }

        void Termination()
        {
            // Release the dependency.  
            SqlDependency.Stop(@"Data Source=BEK-47674357\sqlexpress;Initial Catalog=NotificationList;Integrated Security=True");
        }
    }
}