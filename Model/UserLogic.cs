using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryDTO;

namespace Model
{
    public class UserLogic
    {
        private UserDAO _userDAO;

        public UserLogic()
        {
            _userDAO = new UserDAO();
        }
        
        public bool AddUser(UserDTO user)
        {
            // Add logic here to pass the user DTO to the data access layer
            // Example:
            return _userDAO.AddUser(user);
        }
        public bool UpdateUser(int userId, UserDTO user)
        {
            // Add logic here to pass the user DTO to the data access layer
            // Example:
            return _userDAO.UpdateUser(userId, user);
        }
        public bool DeleteUser(UserDTO userId)
        {
            // Add logic here to pass the user DTO to the data access layer
            // Example:
            return _userDAO.DeleteUser(userId.Uid);
        }

        public List<UserDTO> GetAllUsers()
        {
            List<User> users = _userDAO.GetAllUsers();

            //transfer the Entity data into the DTO
            List<UserDTO> userDTOs = new List<UserDTO>();

            foreach (User user in users)
            {
                UserDTO userDTO = new UserDTO();
                userDTO.Uid = user.Uid;
                userDTO.UserName = user.UserName;
                userDTO.Password = user.Password;
                userDTO.UserLevel = user.UserLevel;

                userDTOs.Add(userDTO);
            }

            return userDTOs;
        }

        public User ValidateLogin(string UserName, string password)
        {
            return _userDAO.GetUserByCredentials(UserName, password);
        }

        // Removed the ValidateUser method as it is not implemented.
        // If you need it, you can add it back and provide its proper implementation.
        
    }


}
