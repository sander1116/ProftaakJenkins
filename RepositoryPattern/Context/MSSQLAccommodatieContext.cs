using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;
using System.Data.SqlClient;

namespace RepositoryPattern.Context
{
    internal class MSSQLAccommodatieContext : Database, IAccommodatieContext
    {
        public List<Accommodatie> GetByFilter(string land, Season season, DateTime vertrekdatum)
        {

            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Reistype = @season and ac.Locatie = @land and v.Datum BETWEEN @vertrekdatum1 AND @vertrekdatum2";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@season", season.ToString());
            cmnd.Parameters.AddWithValue("@land", land);
            cmnd.Parameters.AddWithValue("@vertrekdatum1", vertrekdatum.Date.ToString("yyyy-MM-dd"));
            cmnd.Parameters.AddWithValue("@vertrekdatum2", vertrekdatum.Date.AddMonths(1).ToString("yyyy-MM-dd"));
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }


        public List<Accommodatie> GetTop5Accommodaties()
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select distinct ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Reistype = 'Zomer'";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }
        public List<Accommodatie> GetByFilterLand(string searchText)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select distinct ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.locatie = @locatie";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@locatie", searchText);
            data = this.Select(cmnd);

            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public List<Accommodatie> GetByFilterSeason(Season season)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select distinct ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Reistype = @season";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@season", season.ToString());
            data = this.Select(cmnd);

            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public string GetTekst()
        {
            string query = "Select Titel from Accommodatie where id = 1";
            string tekst = "";
            using (SqlConnection connection = new SqlConnection(Database.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        tekst = (string)reader[0];
                    }
                }
            }
            return tekst;
        }

        public Accommodatie GetAccommodatie(string title)
        {
            //todo: query verkleinen
            string query = "Select * from Accommodatie where titel = '" + title + "'";
            DataTable data = new DataTable();
            Accommodatie accommodatie = new Accommodatie();
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            using (SqlConnection connection = new SqlConnection(Database.connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        accommodatie.ID = (int)reader[0];
                        accommodatie.Title = (string)reader[1];
                        accommodatie.Description = (string)reader[2];
                        accommodatie.Country = (string)reader[3];
                        accommodatie.Volwasseneprijs = (decimal)reader[5];
                        accommodatie.Kinderprijs = (decimal)reader[6];
                    }
                }
            }
            data.Dispose();
            query = "Select aa.Link from Accommodatie ac left join AccommodatieAfbeeldingen aa on ac.ID=aa.Accommodatie_ID where ac.Titel=@Titel";
            cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@Titel", title);
            data = this.Select(cmnd);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow picture in data.Rows)
                {
                    accommodatie.Picture.Add(Convert.ToString((picture["Link"])));
                }
            }
            data.Dispose();
            query = "Select v.Datum from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID where ac.Titel= @Titel";
            cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@Titel", title);
            data = this.Select(cmnd);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow datum in data.Rows)
                {
                    accommodatie.DepartureDate.Add(Convert.ToDateTime(datum["Datum"]));
                }
            }
            return accommodatie;
        }

        public List<Accommodatie> SearchBar(string zoekTekst)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = $"Accommodatie_SearchBar";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("@SearchText", zoekTekst);
            data = this.Select(cmnd);

            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            
            return accommodaties;
        }
        private List<Accommodatie> GetKorteBeschrijvingen(List<Accommodatie> accommodaties)
        {
            foreach (Accommodatie accommodatie in accommodaties)
            {
                DataTable data = new DataTable();
                string query = "Select k.Tekst from Accommodatie ac left join KorteBeschrijving k on ac.ID=k.Accommodatie_ID where ac.Titel=@Titel";
                SqlCommand cmnd = new SqlCommand(query, this.Conn);
                cmnd.Parameters.AddWithValue("@Titel", accommodatie.Title);
                data = this.Select(cmnd);

                if (data.Rows.Count > 0)
                {
                    foreach (DataRow korteBeschrijving in data.Rows)
                    {
                        accommodatie.KorteBeschrijving.Add(Convert.ToString((korteBeschrijving["Tekst"])));
                    }
                }
            }
            return accommodaties;
        }
        private List<Accommodatie> GetAccommodatiePictues(List<Accommodatie> accommodaties)
        {
            foreach (Accommodatie accommodatie in accommodaties)
            {
                DataTable data = new DataTable();
                string query = "Select aa.Link from Accommodatie ac left join AccommodatieAfbeeldingen aa on ac.ID=aa.Accommodatie_ID where ac.Titel=@Titel";
                SqlCommand cmnd = new SqlCommand(query, this.Conn);
                cmnd.Parameters.AddWithValue("@Titel", accommodatie.Title);
                data = this.Select(cmnd);

                if (data.Rows.Count > 0)
                {
                    foreach (DataRow picture in data.Rows)
                    {
                        accommodatie.Picture.Add(Convert.ToString((picture["Link"])));
                    }
                }
            }
            return accommodaties;
        }
        private List<Accommodatie> GetDepartureDates(List<Accommodatie> accommodaties)
        {
            foreach (Accommodatie accommodatie in accommodaties)
            {
                DataTable data = new DataTable();
                string query = "Select v.Datum from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID where ac.Titel= @Titel";
                SqlCommand cmnd = new SqlCommand(query, this.Conn);
                cmnd.Parameters.AddWithValue("@Titel", accommodatie.Title);
                data = this.Select(cmnd);

                if (data.Rows.Count > 0)
                {
                    foreach (DataRow datum in data.Rows)
                    {
                        accommodatie.DepartureDate.Add(Convert.ToDateTime(datum["Datum"]));
                    }
                }
            }
            return accommodaties;
        }
        private List<Accommodatie> GetAccommodatie(DataTable data)
        {
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            if (data.Rows.Count > 0)
            {
                foreach (DataRow accommodatie in data.Rows)
                {
                    accommodaties.Add(new Accommodatie(Convert.ToInt32(accommodatie["id"]), accommodatie["Titel"].ToString(), Convert.ToDecimal(accommodatie["Volwasseneprijs"]), Convert.ToDecimal(accommodatie["Kinderprijs"]), accommodatie["Beschrijving"].ToString(), accommodatie["Locatie"].ToString()));
                }
            }
            return accommodaties;
        }

        public List<Accommodatie> GetAllAccommodaties()
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);        
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public List<Accommodatie> GetByFilterDate(DateTime vertrekdatum)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where v.Datum BETWEEN @vertrekdatum1 AND @vertrekdatum2";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@vertrekdatum1", vertrekdatum.Date.ToString("yyyy-MM-dd"));
            cmnd.Parameters.AddWithValue("@vertrekdatum2", vertrekdatum.Date.AddMonths(1).ToString("yyyy-MM-dd"));
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public List<Accommodatie> GetByFilterSeasonLand(Season season, string land)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Reistype = @season and ac.Locatie = @land";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@season", season.ToString());
            cmnd.Parameters.AddWithValue("@land", land);
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public List<Accommodatie> GetByFilterSeasonDate(Season season, DateTime vertrekdatum)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Reistype = @season and v.Datum BETWEEN @vertrekdatum1 AND @vertrekdatum2";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@season", season.ToString());
            cmnd.Parameters.AddWithValue("@vertrekdatum1", vertrekdatum.Date.ToString("yyyy-MM-dd"));
            cmnd.Parameters.AddWithValue("@vertrekdatum2", vertrekdatum.Date.AddMonths(1).ToString("yyyy-MM-dd"));
            data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }

        public List<Accommodatie> GetByFilterLandDate(string land, DateTime vertrekdatum)
        {
            DataTable data = new DataTable();
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            string query = "Select DISTINCT ac.id, ac.Titel, ac.volwasseneprijs, ac.kinderprijs, ac.Beschrijving, ac.Locatie from Accommodatie ac join Accommodatie_VertrekDatum av on ac.ID = av.Accommodatie_ID join VertrekDatum v on av.VertrekDatum_ID = v.ID  where ac.Locatie = @land and v.Datum BETWEEN @vertrekdatum1 AND @vertrekdatum2";
            SqlCommand cmnd = new SqlCommand(query, this.Conn);
            cmnd.Parameters.AddWithValue("@land", land);
            cmnd.Parameters.AddWithValue("@vertrekdatum1", vertrekdatum.Date.ToString("yyyy-MM-dd"));
            cmnd.Parameters.AddWithValue("@vertrekdatum2", vertrekdatum.Date.AddMonths(1).ToString("yyyy-MM-dd")); data = this.Select(cmnd);
            accommodaties = GetAccommodatie(data);
            accommodaties = GetDepartureDates(accommodaties);
            accommodaties = GetAccommodatiePictues(accommodaties);
            accommodaties = GetKorteBeschrijvingen(accommodaties);
            return accommodaties;
        }
    }
}
