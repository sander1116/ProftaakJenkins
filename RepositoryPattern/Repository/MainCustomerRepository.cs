using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;
using RepositoryPattern.Context;

namespace RepositoryPattern.Repository
{
    public class MainCustomerRepository 
    {
        private IMainCustomerContext iMainCustomerContext;

        public MainCustomerRepository(IMainCustomerContext IMainCustomerContext)
        {
            iMainCustomerContext = IMainCustomerContext;
        }

        bool Insert(MainCustomer maincustomer)
        {
            return iMainCustomerContext.Insert(maincustomer);
        }
    }
}
