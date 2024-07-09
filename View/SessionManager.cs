using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    
        public static class SessionManager
        {
            public static bool UserIsLoggedIn { get; private set; }
            public static User CurrentUser { get; private set; }

            public static void Login(User user)
            {
                CurrentUser = user;
                UserIsLoggedIn = true;
                // You can also add more login logic here if needed.
            }

            public static void Logout()
            {
                CurrentUser = null;
                UserIsLoggedIn = false;
                // Additional logout logic can be placed here.
            }
        }

    }

