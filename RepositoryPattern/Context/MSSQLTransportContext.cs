using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Context
{
    public class MSSQLTransportContext : ItransportContext
    {
        public Transport GetByNumber(Boeking boeking)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Transport transport)
        {
            throw new NotImplementedException();
        }
    }
}
