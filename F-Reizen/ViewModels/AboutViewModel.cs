using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Repository;
using RepositoryPattern.IContext;
using RepositoryPattern.Context;
using RepositoryPattern.Factory;

namespace PresentationLayer.ViewModels
{
    public class AboutViewModel
    {
        public string Tekst { get; set; }
        public AccommodatieRepository test;
        public IAccommodatieContext test2;
        public AboutViewModel()
        {
            test = new AccommodatieRepository(AccommodatieFactory.Get(1));
            Tekst = test.GetTekst();
        }
    }
}
