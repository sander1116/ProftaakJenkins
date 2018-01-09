using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.IContext;
using RepositoryPattern.Context;
using F_Reizen.Models;

namespace RepositoryPattern.Repository
{
    public class TravelpartnerRepository
    {
        private ITravelpartnerContext iTravelpartnerContext;

        public TravelpartnerRepository(ITravelpartnerContext ITravelpartnerContext)
        {
            iTravelpartnerContext = ITravelpartnerContext;
        }

        public bool Insert(Travelpartner Travelpartner)
        {
            return iTravelpartnerContext.Insert(Travelpartner);
        }
    }
}
