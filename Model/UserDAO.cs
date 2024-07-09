using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryDTO;
using Model.DataSetUserTableAdapters;

namespace Model
{
    public class UserDAO
    {
        private DataSetUserTableAdapters.TabUserTableAdapter _tabUserTableAdapter;
        public UserDAO()
        {
            _tabUserTableAdapter = new DataSetUserTableAdapters.TabUserTableAdapter();
        }
        
        public bool AddUser(UserDTO user)
        {
            try
            {
                // Assuming your DataSetUser has a method Insert that returns the number of rows affected.
                int rowsAffected = _tabUserTableAdapter.Insert(
                    user.UserName,
                    user.Password, // Ensure this is hashed if it's sensitive data.
                    user.UserLevel              // ... other parameters as needed ...
                );

                // If the insert command was successful, it should return 1 row affected.
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework or strategy
                // For now, just write to the console or debug output
                Console.WriteLine(ex.Message);
                return false; // Return false if an exception occurred
            }
        }

        public bool UpdateUser(int userId, UserDTO updatedUser)
        {
            try
            {
                // Retrieve the original user details before the update for concurrency check
                var originalUser = _tabUserTableAdapter.GetDataByUserId(updatedUser.Uid).FirstOrDefault();
                if (originalUser == null)
                {
                    // Handle the case where the user no longer exists, if necessary
                    return false;
                }

                // The Update method may need all the user fields, plus the original UID
                int rowsAffected = _tabUserTableAdapter.Update(
                    updatedUser.UserName,
                    updatedUser.Password, 
                    updatedUser.Uid // The unique identifier for the user to be updated
                    //originalUser.UID, // Original UID for concurrency check
                    //originalUser.UserName,
                    //originalUser.Password
                // ... additional original fields ...
                );

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework or strategy
                Console.WriteLine(ex.Message);
                return false; // Return false if an exception occurred
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                // Retrieve the original user details for concurrency check before deletion
                var originalUser = _tabUserTableAdapter.GetDataByUserId(userId).FirstOrDefault();
                if (originalUser == null)
                {
                    // Handle the case where the user no longer exists, if necessary
                    return false;
                }

                // The Delete method may need the original values to perform a concurrency check
                int rowsAffected = _tabUserTableAdapter.Delete(
                    userId,
                    originalUser.UserName, // Original user name for concurrency check
                    originalUser.Password, // Original password for concurrency check
                    originalUser.UserLevel
                    //originalUser // Original UID for concurrency check, if required
                );

                return rowsAffected >0 ;
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging framework or strategy
                Console.WriteLine(ex.Message);
                return false; // Return false if an exception occurred
            }
        }


        public List<User> GetAllUsers()
        {
            //connects to the Database
            //and executing the Query and it return the data into the object tabUserDataTable
            TabUserTableAdapter tabUserTableAdapter = new TabUserTableAdapter();
            DataSetUser.TabUserDataTable tabUserDataTable = tabUserTableAdapter.GetAllUsers();

            //now traverse the tabUserDataTable and get all the data one by one (loop)
            //1st check if any data is returned
            int dataCount = tabUserDataTable.Count;
            if (dataCount == 0)
            {
                //this means there is not data in the table
                return null;
            }
            else
            {
                //create a List of User objects
                List<User> users = new List<User>();

                //there are data, so now traverse the tabUserDataTable and get all the data one by one (loop)
                foreach (DataRow row in tabUserDataTable)
                {
                    int uid = Convert.ToInt32(row["UID"]);
                    string userName = row["UserName"].ToString();
                    string password = row["Password"].ToString();
                    int userLevel = Convert.ToInt32(row["UserLevel"]);

                    //encapsulat the above data into a User Object (for this 1st we have to create an Entity class called User)
                    User user = new User();
                    user.Uid = uid;
                    user.UserName = userName;
                    user.Password = password;
                    user.UserLevel = userLevel;

                    //and then add that User Object into a List
                    users.Add(user);
                }

                //return the List of User Objects
                return users;
            }
        }

        public User ValidateLogin(string sUserName, string password)
        {
            //connects to the Database
            //and executing the Query and it return the data into the object tabUserDataTable
            TabUserTableAdapter tabUserTableAdapter = new TabUserTableAdapter();
            DataSetUser.TabUserDataTable tabUserDataTable = tabUserTableAdapter.ValideLogin(sUserName, password);

            int dataCount = tabUserDataTable.Count;
            if (dataCount == 0)
            {
                //this means invalid username
                return null;
            }
            else
            {
                //valid username
                //create an object of User entity class
                User user = new User();

                //access the returned row (i.e. row number 1) which is inside the tabUserDataTable object
                DataRow userDataRow = tabUserDataTable.Rows[0];

                user.Uid = Convert.ToInt32(userDataRow["UID"]);
                user.UserName = userDataRow["UserName"].ToString();
                user.Password = userDataRow["Password"].ToString();
                user.UserLevel = Convert.ToInt32(userDataRow["UserLevel"]);

                return user;
            }
        }
        public User GetUserByCredentials(string sUserName, string password)
        {
            TabUserTableAdapter tabUserTableAdapter = new TabUserTableAdapter();
            
            DataSetUser.TabUserDataTable tabUserDataTable = tabUserTableAdapter.GetUserByCredentials(sUserName, password);

            int dataCount = tabUserDataTable.Count;
            if (dataCount == 0)
            {
                // No user found with the given credentials
                return null;
            }
            else
            {
                // User found
                User user = new User();
                DataRow userDataRow = tabUserDataTable.Rows[0];

                user.Uid = Convert.ToInt32(userDataRow["UID"]);
                user.UserName = userDataRow["UserName"].ToString();
                user.Password = userDataRow["Password"].ToString();
                user.UserLevel = Convert.ToInt32(userDataRow["UserLevel"]);

                return user;
            }
        }


    }
}
