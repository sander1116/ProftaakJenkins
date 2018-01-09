using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Context
{
    internal class Database
    {

        public static string connectionString = @"Server=tcp:f-reizen.database.windows.net,1433;Initial Catalog=F-Reizen;Persist Security Info=False;User ID=AdminF-Reizen@f-reizen.database.windows.net;Password=ZGEHwx10;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SqlConnection Conn = new SqlConnection(connectionString);

        public bool OpenConnection()
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseConnection()
        {
            if (Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }
        }

        public void Update(SqlCommand cmnd)
        {
            try
            {
                OpenConnection();
                cmnd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new DatabaseError();
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Delete(SqlCommand cmnd)
        {
            try
            {
                OpenConnection();
                cmnd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new DatabaseError();
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Insert(SqlCommand cmnd)
        {
            try
            {
                OpenConnection();
                cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DatabaseError();
            }
            finally
            {
                CloseConnection();
            }
        }

        public int InsertGetLastId(SqlCommand cmnd)
        {
            try
            {
                OpenConnection();
                return (int)cmnd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw new DatabaseError();
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable Select(SqlCommand cmnd)
        {
            DataTable resultTable = new DataTable();

            try
            {
                OpenConnection();
                using (SqlDataReader reader = cmnd.ExecuteReader())
                {
                    resultTable.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseError();
            }
            finally
            {
                CloseConnection();
            }
            return resultTable;
        }
    }
}
