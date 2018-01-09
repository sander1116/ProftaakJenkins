  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace F_Reizen.Models
{
    public class Transport
    {
        //Fields
        private bool ownTransport;
        private decimal travelcost;
        private string airport;
        private int id;

        //Properties
        public bool OwnTransport
        {
            get { return ownTransport; }
            set { ownTransport = value; }
        }
        public decimal Travelcost
        {
            get { return travelcost; }
            set { travelcost = value; }
        }
        public string Airport
        {
            get { return airport; }
            set { airport = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //constructor
        public Transport()
        {
            ownTransport = true;
        }
        public Transport(decimal Travelcost, string Airport)
        {
            ownTransport = false;
            travelcost = Travelcost;
            airport = Airport;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            if(ownTransport)
            {
                sb.AppendLine("Car");
            }
            else
            {
                sb.AppendLine("Plane");
                sb.AppendLine(travelcost.ToString());
                sb.AppendLine(airport);
            }

            return sb.ToString();

        }

    }
}