using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColorApplication.Models
{
    public class Notification
    {

        public void AddNotification(string Text, int UserName)
        {
            
            using (Model1 ent = new Model1())
            {
                
                NotificationList obj = new NotificationList();
                obj.Text = Text;
                obj.UserId = UserName;
                obj.CreatedDate = DateTime.Now.ToUniversalTime();
                ent.NotificationLists.Add(obj);
                ent.SaveChanges();
            }
        }

        public List<NotificationList> GetNotifications(int userName)
        {

            using (Model1 ent = new Model1())
            {
                return ent.NotificationLists.Where(e => e.UserId == userName).ToList();
            }
        }

        public List<NotificationList> GetLatestNotifications(DateTime dt)
        {

            using (Model1 ent = new Model1())
            {
                if (dt == DateTime.MinValue)
                {
                    return ent.NotificationLists.ToList();
                }
                else
                {
                    DateTime dtUTC = dt.ToUniversalTime();
                    return ent.NotificationLists.Where(e => e.CreatedDate > dtUTC).ToList();
                }
            }
        }
    }
}