using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Context
{
    public class MSSQLTravelpartnerContext : ITravelpartnerContext
    {
        public bool Insert(Travelpartner Travelpartner)
        {
            throw new NotImplementedException();
        }
    }
}
