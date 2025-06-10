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

        public static void Login (string username)
        {
            IsLoggedIn = true;
            CurrentUser = username;
        }

        public static void Logout(string username)
        {
            IsLoggedIn = false;
            CurrentUser = null;
        }
    }
}
