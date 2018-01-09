using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;
using System.Data.SqlClient;
using System.Data;

namespace RepositoryPattern.Context
{
    internal class MSSQLBoekingContext : Database,IboekingContext
    {
        public bool Delete(Boeking boeking)
        {
            string query = "delete from boeking where id = "+boeking.Number;
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            return true;
        }

        public bool Insert(MainCustomer mainCustomer, List<Travelpartner> travelPartners)
        {
            try
            {
                mainCustomer = InsertMainCustomer(mainCustomer);

                foreach (Travelpartner travelpartner in travelPartners)
                {
                    if (travelpartner == travelPartners[0])
                    {
                        InsertTravelPartner(travelpartner, true, mainCustomer);
                    }
                    else
                    {
                        InsertTravelPartner(travelpartner, false, mainCustomer);
                    }
                }

                mainCustomer.Boeking.Transport = InsertTransport(mainCustomer.Boeking.Transport);

                InsertBoeking(mainCustomer);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MainCustomer InsertMainCustomer(MainCustomer mainCustomer)
        {
            string QUERY = $"Boeking_InsertHoofdboeker";

            SqlCommand CMD = new SqlCommand(QUERY, Conn);

            //To prevent SQL-injection
            CMD.Parameters.AddWithValue("@Naam", mainCustomer.Firstname + " " + mainCustomer.Lastname);
            CMD.Parameters.AddWithValue("@Aanhef", mainCustomer.Preamble);
            CMD.Parameters.AddWithValue("@Geboortedatum", mainCustomer.DateOfBirth);
            CMD.Parameters.AddWithValue("@Email", mainCustomer.Email);
            CMD.Parameters.AddWithValue("@Adres", mainCustomer.Adress);
            CMD.Parameters.AddWithValue("@Stad", mainCustomer.Residence);

            CMD.CommandType = CommandType.StoredProcedure;

            try
            {
                if (OpenConnection())
                {
                    mainCustomer.ID = Convert.ToInt32(CMD.ExecuteScalar());
                }

            }
            catch (SqlException exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                CloseConnection();
            }
            return mainCustomer;
        }


        public void InsertTravelPartner(Travelpartner travelpartner, bool volwassene, MainCustomer mainCustomer)
        {
            string QUERY = $"Boeking_InsertTravelPartner";

            SqlCommand CMD = new SqlCommand(QUERY, Conn);

            //To prevent SQL-injection
            CMD.Parameters.AddWithValue("@Naam", travelpartner.Firstname + " " + travelpartner.Lastname);
            CMD.Parameters.AddWithValue("@Aanhef", travelpartner.Preamble);
            CMD.Parameters.AddWithValue("@Geboortedatum", travelpartner.DateOfBirth);
            CMD.Parameters.AddWithValue("@Volwassenen", volwassene);
            CMD.Parameters.AddWithValue("@Hoofdboeker_ID", mainCustomer.ID);

            CMD.CommandType = CommandType.StoredProcedure;

            try
            {
                if (OpenConnection())
                {
                    CMD.ExecuteNonQuery();
                }

            }
            catch (SqlException exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                CloseConnection();
            }
        }


        public Transport InsertTransport(Transport transport)
        {
            string QUERY = $"Boeking_InsertVervoer";

            SqlCommand CMD = new SqlCommand(QUERY, Conn);

            //To prevent SQL-injection
            CMD.Parameters.AddWithValue("@Reiskosten", transport.Travelcost);
            
            if (transport.OwnTransport)
            {
                CMD.Parameters.AddWithValue("@Vertrekpunt", DBNull.Value);
            }
            else
            {
                CMD.Parameters.AddWithValue("@Vertrekpunt", transport.Airport);
            }

            CMD.CommandType = CommandType.StoredProcedure;

            try
            {
                if (OpenConnection())
                {
                    transport.ID = Convert.ToInt32(CMD.ExecuteScalar());
                }

            }
            catch (SqlException exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                CloseConnection();
            }
            return transport;
        }


        public MainCustomer InsertBoeking(MainCustomer mainCustomer)
        {
            string QUERY = $"Boeking_InsertBoeking";

            SqlCommand CMD = new SqlCommand(QUERY, Conn);

            //To prevent SQL-injection
            CMD.Parameters.AddWithValue("@Korting", mainCustomer.Boeking.Discount);
            CMD.Parameters.AddWithValue("@AantalKamers", mainCustomer.Boeking.Rooms);
            CMD.Parameters.AddWithValue("@TotaalPrijs", mainCustomer.Boeking.Totalprice);
            CMD.Parameters.AddWithValue("@AnnuleringsVerzekering", mainCustomer.Boeking.CancellationInsurance);
            CMD.Parameters.AddWithValue("@ReisVerzekering", mainCustomer.Boeking.TravelInsurance);

            //NOG TOEVOEGEN
            CMD.Parameters.AddWithValue("@Accommodatie_ID", mainCustomer.Boeking.Accommodatie.ID);
            CMD.Parameters.AddWithValue("@Vervoer_ID", mainCustomer.Boeking.Transport.ID);

            CMD.Parameters.AddWithValue("@Hoofdboeker_ID", mainCustomer.ID);

            CMD.CommandType = CommandType.StoredProcedure;

            try
            {
                if (OpenConnection())
                {
                    CMD.ExecuteNonQuery();
                }

            }
            catch (SqlException exception)
            {
                throw new Exception(exception.ToString());
            }
            finally
            {
                CloseConnection();
            }
            return mainCustomer;
        }
    }
}
