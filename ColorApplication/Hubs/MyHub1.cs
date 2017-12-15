using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ColorApplication.Hubs
{
    public class MyHub1 : Hub
    {
        public void SendNotification(string message, int user)
        {
            //Create an instance of the Repository class
            Models.Notification Repos = new Models.Notification();


            //Invoke the Add Notification method that we created in the repository to add the notification to the database 
            Repos.AddNotification(message, user);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //Create an instance of the Repository class
            Models.Notification objRepository = new Models.Notification();

            //refreshNotification is the client side method which will be writing in the future section. GetLogin() is a static extensions extract just the login name scrapping the domain name 
            Clients.User(Context.User.Identity.Name).refreshNotification(objRepository.GetNotifications("Avil");

            return base.OnConnected();

        }
    }
    public static class Extensions
    {
        public static string GetDomain(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(0, stop) : string.Empty;
        }

        public static string GetLogin(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : string.Empty;
        }
    }
}