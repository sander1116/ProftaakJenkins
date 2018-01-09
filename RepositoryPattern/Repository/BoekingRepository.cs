using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.Context;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Repository
{
    public class BoekingRepository
    {
        private IboekingContext iboekingContext;

        public BoekingRepository(IboekingContext IboekingContext)
        {
            iboekingContext = IboekingContext;
        }

        public bool Delete(Boeking boeking)
        {
            return iboekingContext.Delete(boeking);
        }

        public bool Insert(MainCustomer mainCustomer, List<Travelpartner> travelPartners)
        {
            return iboekingContext.Insert(mainCustomer, travelPartners);
        }
    }
}
