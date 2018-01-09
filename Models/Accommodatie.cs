using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace F_Reizen.Models
{
    public class Accommodatie
    {
        //Fields
        private int id;
        private string title;
        private decimal volwasseneprijs;
        private decimal kinderprijs;
        private string description;
        private List<string> picture;
        private string country;
        private List<DateTime> departureDate;
        private List<string> korteBeschrijving;
        

        //Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public decimal Volwasseneprijs
        {
            get { return volwasseneprijs; }
            set { volwasseneprijs = value; }
        }
        public decimal Kinderprijs
        {
            get { return kinderprijs; }
            set { kinderprijs = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public List<string> Picture
        {
            get { return picture; }
            set { picture = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public List<DateTime> DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; }
        }

        public List<string> KorteBeschrijving
        {
            get { return korteBeschrijving; }
            set { korteBeschrijving = value; }
        }

        //Constructor
        public Accommodatie(int Id, string Title, decimal Volwasseneprijs, decimal Kinderprijs, string Description, string Country)
        {
            ID = Id;
            departureDate = new List<DateTime>();
            picture = new List<string>();
            korteBeschrijving = new List<string>();
            title = Title;
            volwasseneprijs = Volwasseneprijs;
            kinderprijs = Kinderprijs;
            description = Description;
            country = Country;
        }
        public Accommodatie()
        {
            departureDate = new List<DateTime>();
            picture = new List<string>();
            korteBeschrijving = new List<string>();
        }

        
    }
}