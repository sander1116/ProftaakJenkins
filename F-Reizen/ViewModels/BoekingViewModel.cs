using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;

namespace PresentationLayer.ViewModels
{
    public class BoekingViewModel
    {
        private List<Travelpartner> travelpartners;
        private MainCustomer maincustomer;
        private Preamble mvr;
        private Preamble dhr;

        public List<Travelpartner> Travelpartner
        {
            get { return travelpartners; }
            set { travelpartners = value; }
        }
        public MainCustomer MainCustomer
        {
            get { return maincustomer; }
            set { maincustomer = value; }
        }
        public Preamble Mvr
        {
            get { return mvr; }
            set { mvr = value; }
        }
        public Preamble Dhr
        {
            get { return dhr; }
            set { dhr = value; }
        }

        public BoekingViewModel()
        {
            travelpartners = new List<Travelpartner>();
            maincustomer = new MainCustomer();
            mvr = Preamble.mvr;
            dhr = Preamble.dhr;
        }
    }
}
