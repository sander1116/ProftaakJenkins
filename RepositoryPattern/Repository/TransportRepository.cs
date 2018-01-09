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
    public class TransportRepository
    {
        private ItransportContext itransportcontext;

        public TransportRepository(ItransportContext ItransportContext)
        {
           itransportcontext = ItransportContext;
        }

        public Transport GetByNumber(Boeking boeking)
        {
            return itransportcontext.GetByNumber(boeking);
        }

        public bool Insert(Transport transport)
        {
            return itransportcontext.Insert(transport);
        }
    }
}
