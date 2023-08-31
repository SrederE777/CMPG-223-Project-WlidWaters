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

        public static Employee getValue()
        {
            return currentUser;
        }

        public static bool Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            string sql =  "";
            SqlParameter[] parameters =
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            currentUser = DataBaseFuncitons.getUser<Employee>(sql, parameters);

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
