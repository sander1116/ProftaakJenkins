using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace F_Reizen.Models
{
    public class Travelpartner : Customer
    {
        //Fields
        private bool adult;

        //Constructor
        public Travelpartner(bool Adult, Preamble Preamble, DateTime DateOfBirth, string Firstname, string Lastname) : base(Preamble, DateOfBirth, Firstname, Lastname)
        {
            adult = Adult;
        }

        public Travelpartner()
        {

        }

        //Methodes
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(preamble.ToString());
            sb.AppendLine(dateOfBirth.Date.ToString());
            sb.AppendLine(firstname);
            sb.AppendLine(lastname);

            if (adult)
            {
                sb.AppendLine("Adult");
            }
            else
            {
                sb.AppendLine("Minor");
            }


            return sb.ToString();
        }
    }
}