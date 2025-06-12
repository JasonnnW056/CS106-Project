using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS106_Project.Classes
{
    public class LoginManager
    {
        public static bool IsLoggedIn { get; private set; }
        public static string CurrentUser { get; private set; }

        public static string UserID { get; private set; }

        public static void Login (string username, string userID)
        {
            IsLoggedIn = true;
            CurrentUser = username;
            UserID = userID;
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = string.Empty;
            UserID = string.Empty;
        }
    }
}
