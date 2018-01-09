using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using F_Reizen.Models;

namespace PresentationLayer.ViewModels
{
    public class AccommodatieViewModel
    {
        public Accommodatie Accommodatie { get; set; }
        public int AantalKinderen { get; set; }
        public int AantalKamers { get; set; }
        public DateTime Datum { get; set; }
    }
}