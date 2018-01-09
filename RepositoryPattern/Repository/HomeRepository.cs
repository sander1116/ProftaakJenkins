using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.IContext;

namespace RepositoryPattern.Repository
{
    public class HomeRepository
    {
        private IHomeContext iHomeContext;

        public HomeRepository(IHomeContext iHomeContext)
        {
            this.iHomeContext = iHomeContext;
        }

        public List<string> GetLanden()
        {
            return iHomeContext.GetLanden();
        }
    }
}
