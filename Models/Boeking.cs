using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Globalization;

namespace F_Reizen.Models
{

    public class Boeking
    {
        //Fields
        private int number;
        private DateTime departure;
        private bool discount;
        private decimal totalprice;
        private int rooms;
        private int amountTravelers;
        private bool travelInsurance;
        private bool cancellationInsurance;
        private Transport transport;
        private Accommodatie accommodatie;
        int[] months = new int[12];

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public DateTime Departure
        {
            get { return departure; }
            set { departure = value; }
        }

        //Properties
        public bool Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public decimal Totalprice
        {
            get { return totalprice; }
            set { totalprice = value; }
        }
        public int Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }
        public int AmountTravelers
        {
            get { return amountTravelers; }
            set { amountTravelers = value; }
        }
        public bool TravelInsurance
        {
            get { return travelInsurance; }
            set { travelInsurance = value; }
        }
        public bool CancellationInsurance
        {
            get { return cancellationInsurance; }
            set { cancellationInsurance = value; }
        }
        public Transport Transport
        {
            get { return transport; }
            set { transport = value; }
        }
        public Accommodatie Accommodatie
        {
            get { return accommodatie; }
            set { accommodatie = value; }
        }

        //Constructor
        public Boeking(int Number, bool Discount, decimal Totalprice, int Rooms, int AmountTravelers, bool TravelInsurance, bool CancellationInsurance)
        {
            number = Number;
            discount = Discount;
            totalprice = Totalprice;
            rooms = Rooms;
            amountTravelers = AmountTravelers;
            travelInsurance = TravelInsurance;
            cancellationInsurance = CancellationInsurance;

            months[0] = 120;
            months[1] = 110;
            months[2] = 100;
            months[3] = 110;
            months[4] = 120;
            months[5] = 130;
            months[6] = 120;
            months[7] = 110;
            months[8] = 100;
            months[9] = 110;
            months[10] = 120;
            months[11] = 130;


        }
        public Boeking()
        {
            transport = new Transport();
            accommodatie = new Accommodatie();
            departure = new DateTime();

            months[0] = 120;
            months[1] = 110;
            months[2] = 100;
            months[3] = 110;
            months[4] = 120;
            months[5] = 130;
            months[6] = 120;
            months[7] = 110;
            months[8] = 100;
            months[9] = 110;
            months[10] = 120;
            months[11] = 130;
        }
        //Methodes
        public decimal Prijsberekening(bool korting)
        {

            ////DateTime month = (DateTime)accommodatie.DepartureDate;
            //string month = accommodatie.DepartureDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("nl"));

            //(Month)Enum.Parse(typeof())

            int toeslag = 50; //aanpassen
            int travelinsuranceprijs = 20;
            int cancellationprijs = 30;
            int vliegticket = 150;
            decimal calamiteitenfonds = 2.50M;
            decimal reserveringskosten = 18.0M;

            decimal partyPrice = GetAdultPrice();

            partyPrice = partyPrice + GetChildrenPrice();

            if (rooms == 3)
            {
                partyPrice = partyPrice + toeslag;
            }
            if (travelInsurance)
            {
                partyPrice = partyPrice + travelinsuranceprijs;
            }
            if (cancellationInsurance)
            {
                partyPrice = partyPrice + cancellationprijs;
            }

            if (transport.OwnTransport == false)
            {
                partyPrice = partyPrice + (vliegticket * (amountTravelers + 2));
            }

            if (korting)
            {
                partyPrice = partyPrice * 0.95M;
            }

            totalprice = partyPrice + calamiteitenfonds + reserveringskosten;
            
            return totalprice;
        }
        public decimal GetAdultPrice()
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string monthname = mfi.GetMonthName(departure.Month).ToString();
            Month month = (Month)Enum.Parse(typeof(Month), monthname);

            decimal AdultPrice; 

            AdultPrice = (accommodatie.Volwasseneprijs * 2) * months[(int)month] / 100;

            return AdultPrice;
        }
        public decimal GetChildrenPrice()
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string monthname = mfi.GetMonthName(departure.Month).ToString();
            Month month = (Month)Enum.Parse(typeof(Month), monthname);

            decimal ChildrenPrice;

            ChildrenPrice = (accommodatie.Kinderprijs * amountTravelers) * months[(int)month] / 100;

            return ChildrenPrice;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(number.ToString());

            if (discount)
            {
                sb.AppendLine("5%");
            }
            sb.AppendLine(totalprice.ToString());
            sb.AppendLine(rooms.ToString());

            if (travelInsurance)
            {
                sb.AppendLine("Travel insurance applied");
            }
            if (cancellationInsurance)
            {
                sb.AppendLine("Cancellation insurance applied");
            }

            return sb.ToString();

        }
    }
}