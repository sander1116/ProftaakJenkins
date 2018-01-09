using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F_Reizen.Models
{
    abstract public class Customer
    {
        //Fields
        protected Preamble preamble;
        protected DateTime dateOfBirth;
        protected string firstname;
        protected string lastname;
        protected Boeking boeking;

        //Properties
        public Preamble Preamble
        {
            get { return preamble; }
            set { preamble = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public Boeking Boeking
        {
            get { return boeking; }
            set { boeking = value; }
        }

        //Constructor
        public Customer()
        {
            boeking = new Boeking();
        }
        public Customer(Preamble Preamble, DateTime DateOfBirth, string Firstname, string Lastname)
        {
            preamble = Preamble;
            dateOfBirth = DateOfBirth;
            firstname = Firstname;
            lastname = Lastname;
        }

        //Methodes
        public abstract override string ToString();
    }
}