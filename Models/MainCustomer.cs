using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace F_Reizen.Models
{
    public class MainCustomer : Customer
    {
        //Fields
        private string adress;
        private string residence;
        private string telephonenumber;
        private string email;
        private int id;
        
        //Properties
        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }
        public string Residence
        {
            get { return residence; }
            set { residence = value; }
        }
        public string Telephonenumber
        {
            get { return telephonenumber; }
            set { telephonenumber = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //Constructor
        public MainCustomer()
        {

        }
        public MainCustomer(string Adress,string Residence, string Telephonenumber, string Email, Preamble Preamble, DateTime DateOfBirth , string Firstname, string Lastname ) :base(Preamble, DateOfBirth, Firstname, Lastname)
        {
            adress = Adress;
            residence = Residence;
            telephonenumber = Telephonenumber;
            email = Email;
        }

        //Methodes
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(preamble.ToString());
            sb.AppendLine(dateOfBirth.Date.ToString());
            sb.AppendLine(firstname);
            sb.AppendLine(lastname);
            sb.AppendLine(adress);
            sb.AppendLine(residence);
            sb.AppendLine(telephonenumber);
            sb.AppendLine(email);

            return sb.ToString();
        }
    }
}