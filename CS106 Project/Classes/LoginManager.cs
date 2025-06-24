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

        public static bool IsAdmin { get; private set; }
        public static string? CurrentUser { get; private set; }

        public static string? UserID { get; private set; }

        public static void Login (string username, string userID, bool isAdmin)
        {
            IsLoggedIn = true;
            CurrentUser = username;
            UserID = userID;
            IsAdmin = isAdmin;
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = string.Empty;
            UserID = string.Empty;
            IsAdmin = false;
        }
    }
}
