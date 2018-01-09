using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;

namespace RepositoryPattern.IContext
{
    public interface ItransportContext
    {
        bool Insert(Transport transport);
        Transport GetByNumber(Boeking boeking);
    }
}
