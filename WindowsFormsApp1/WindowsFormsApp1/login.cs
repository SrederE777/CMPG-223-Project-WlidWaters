using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class login
    {
        private static Employee currentUser;
        private static int accessLevel;
        private static Dictionary<string, int> accessLevels = new Dictionary<string, int>
            { {"Admin", 0},
              {"User", 1}
            };

        //Dictionary<string, >

        public static Employee getValue()
        {
            return currentUser;
        }

        public static int getAccessLevel()
        {
            return accessLevel;
        }

        public static bool Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (username == "Admin" && password == "Admin")
            {
                if (accessLevels.ContainsKey(username))
                {
                    accessLevel = accessLevels[username];
                    return true;
                }

            }

            if (username == "User" && password == "User")
            {
                if (accessLevels.ContainsKey(username))
                {
                    accessLevel = accessLevels[username];
                    return true;
                }
            }
            string sql = "SELECT * FROM Employees WHERE Employee_Name = @username AND Employee_Password = @password";
            SqlParameter[] parameters =
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            currentUser = DataBaseFuncitons.GetData<Employee>(sql, parameters);

            if (currentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
