using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPattern.IContext;
using System.Data.SqlClient;
using System.Data;

namespace RepositoryPattern.Context
{
  internal class MSSQLHomeContext : Database,IHomeContext
  {
    public List<string> GetLanden()
    {
      DataTable data = new DataTable();
      List<string> landen = new List<string>();
      string query = "select distinct locatie from accommodatie order by locatie";
      SqlCommand cmnd = new SqlCommand(query, this.Conn);
      data = this.Select(cmnd);

      if (data.Rows.Count > 0)
      {
        foreach (DataRow land in data.Rows)
        {
          landen.Add(Convert.ToString(land["Locatie"]));
        }
      }
      return landen;
    }
  }
}
