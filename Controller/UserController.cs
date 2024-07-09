using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ClassLibraryDTO;

namespace Controller
{
    public class UserController
    {
        public List<UserDTO> GetAllUsers() {
            UserLogic userLogic = new UserLogic();
            List<UserDTO> users = userLogic.GetAllUsers();
            return users;
        }

        public User ValidateLogin(string UserName, string password)
        {
            UserLogic userLogic = new UserLogic();
            User user = userLogic.ValidateLogin(UserName, password);
            if (user != null && user.Password == password) // Ensure you have proper password hashing and comparison here!
            {
                // The user is valid, return the user object
                return user;
            }
            return user;
        }
        private UserLogic _userLogic = new UserLogic();
       
        public bool AddUser(UserDTO user)
        {
            return _userLogic.AddUser(user);
        }

        public bool UpdateUser(int userId, UserDTO user)
        {
            return _userLogic.UpdateUser(userId, user);
        }

        public bool DeleteUser(int userId, UserDTO user)
        {
            return _userLogic.DeleteUser(user);
        }

        
    }
}
