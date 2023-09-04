using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Customer : Person
    {
        
        private String email;

        public Customer(string email) : base()
        {

        }

        public void setEmail(String email) 
        {
            this.email = email;
        }

        public String getEmail()
        {
            return email;
        }

        
        public String toString()
        {
            return "Email: " + email;
        }
    }
}
