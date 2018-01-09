using System.Data.SqlClient;
using Outsourcing_F_Reizen.Models;


namespace Outsourcing_F_Reizen.Database
{
    public static class DB
    {
        private static string CS = "Server=tcp:f-reizen.database.windows.net,1433;Initial Catalog=F-Reizen;Persist Security Info=False;User ID=AdminF-Reizen@f-reizen.database.windows.net;Password=ZGEHwx10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static Account LoginCheck(Account account)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Reisagent WHERE Inlognaam=@username AND Password=@password", conn);
            cmd.Parameters.AddWithValue("@username", account.Username);
            cmd.Parameters.AddWithValue("@password", account.Password);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                return account;
            }
            else
            {
               return null;
            }
             
        }
    }
}
