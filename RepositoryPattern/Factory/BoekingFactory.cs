using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.IContext;
using RepositoryPattern.Context;

namespace RepositoryPattern.Factory
{
    public class BoekingFactory
    {
        public static IboekingContext Get(int id)
        {
            return new MSSQLBoekingContext();
        }
    }
}
