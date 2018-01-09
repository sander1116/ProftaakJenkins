using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Context
{
    public class MSSQLMainCustomerContext : IMainCustomerContext
    {
        public bool Insert(MainCustomer maincustomer)
        {
            throw new NotImplementedException();
        }
    }
}
