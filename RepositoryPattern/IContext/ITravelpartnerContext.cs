using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;

namespace RepositoryPattern.IContext
{
    public interface ITravelpartnerContext
    {
        bool Insert(Travelpartner Travelpartner);
    }
}
