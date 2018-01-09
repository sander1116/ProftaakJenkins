using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.Context;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Factory
{
  public class HomeFactory
  {

    public static IHomeContext Get(int id)
    {
      return new MSSQLHomeContext();
    }
  }
}
