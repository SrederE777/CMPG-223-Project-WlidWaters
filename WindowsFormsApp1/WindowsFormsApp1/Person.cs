﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Person
    {
        // Properties
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateTime Birthday { get; set; }

        public Person() : this("", "", DateTime.Now)
        {

        }
        // Constructors
        public Person(string name, string contact, DateTime birthday)
        {
            Name = name;
            Contact = contact;
            Birthday = birthday;
        }

        // Methods to set properties
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetContact(string contact)
        {
            Contact = contact;
        }

        public void SetBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }

        // Methods to get properties
        public string GetName()
        {
            return Name;
        }

        public string GetContact()
        {
            return Contact;
        }

        public DateTime GetBirthday()
        {
            return Birthday;
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {Name}\nContact: {Contact}\nBirthday: {Birthday.ToShortDateString()}";
        }
    }
}

