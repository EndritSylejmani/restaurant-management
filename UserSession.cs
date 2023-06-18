using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenagjimiRestorantav
{

    public class UserObject
    {
        public string ID { get; set; }
        public string username { get; set; }
        public string role { get; set; }
    }
    public static class UserSession
    {
        private static object currentUser = new UserObject();
        private static object syncRoot = new Object();

        public static UserObject GetUser()
        {
            /*if (currentUser == null) throw new Exception("Not logged in.");*/
            return (UserObject) currentUser;
        }

        public static void Login(UserObject user)
        {
           /* if (currentUser != null) throw new Exception("Already logged in");*/
            lock (syncRoot)
            {
                currentUser = user;
            }
        }

        public static void Logout()
        {
            lock (syncRoot)
            {
                currentUser = null;
            }
        }
    }
}
