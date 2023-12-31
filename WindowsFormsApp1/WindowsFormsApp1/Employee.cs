﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class foreignKey
    {
        public int value { get; set; }
        public string name { get; set; }
        private string table { get; set; }
        private string IDName { get; set; }
        public foreignKey()
        {
            
        }

        public foreignKey(int value, string table, string IDName)
        {
            this.value = value;
            this.table = table;
            this.IDName = IDName;
            name = getName(this.table, this.IDName);
        }

        private string getName(string table, string IDName)
        {
            string sql = "SELECT "+ IDName+ "_Name FROM "+ table + " WHERE " + IDName + "_ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", value) };

            string RideName = DataBaseFuncitons.queryDataBase(sql,parameters);
            if (RideName != null)
                return RideName;
            else
                return "";
        }

        public override string ToString()
        {
            return $"Key: {value}";
        }

    }

    class Employee : DataClasses
    {
        // Properties
        public string Employee_Name { get; set; }
        public string Employee_Surname { get; set; }
        public string Employee_Emergency_Contact { get; set; }
        public string Employee_Contact { get; set; }
        public string Employee_Password { get; set; }
        public foreignKey Ride_ID { get; set; }

        public Employee() : this("","", "", "", "", new foreignKey(1,"Rides", "Ride"))
        {
            // Initialize properties with default values if desired
        }

        // Constructors
        public Employee(string emergencyContact, string password, foreignKey ride) : base()
        {
            Employee_Emergency_Contact = emergencyContact;
            Employee_Password = password;
            Ride_ID = ride;
        }

        // Constructor that takes parameters for base class properties
        public Employee(string name, string surname, string contact, string emergencyContact, string password, foreignKey ride)
        {
            Employee_Name = name;
            Employee_Surname = surname;
            Employee_Contact = contact;
            Employee_Emergency_Contact = emergencyContact;
            Employee_Password = password;
            Ride_ID = ride;
        }

        public List<string> getName()
        {
            List<string> names = new List<string>();
            names.Add("Name");
            names.Add("Surname");
            names.Add("Contact");
            names.Add("Emergency Contact");
            names.Add("Password");
            names.Add("Ride");
            return names;

        }

        public List<string> getDataBaseName()
        {
            List<string> names = new List<string>();
            names.Add("Employee_Name");
            names.Add("Employee_Surname");
            names.Add("Employee_Emergency_Contact");
            names.Add("Employee_Contact");
            names.Add("Employee_Password");
            names.Add("Ride_ID");
            return names;

        }



        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {Employee_Name}\nSurname: {Employee_Surname}\nContact: {Employee_Contact}\nEmergency Contact: {Employee_Emergency_Contact}\nPassword: {Employee_Password}\nRide Name: {Ride_ID.name}";
        }

    }


}