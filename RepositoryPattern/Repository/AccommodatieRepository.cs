using System;
using System.Collections.Generic;
using F_Reizen.Models;
using RepositoryPattern.IContext;
using RepositoryPattern.Context;

namespace RepositoryPattern.Repository
{
    public class AccommodatieRepository
    {
        private IAccommodatieContext iaccommodatiecontext;

        public AccommodatieRepository(IAccommodatieContext IAccommodatieContext)
        {
            iaccommodatiecontext = IAccommodatieContext;
        }
        public List<Accommodatie> GetByFilterLand(string searchText)
        {
            return iaccommodatiecontext.GetByFilterLand(searchText);
        }

        public List<Accommodatie> GetAllAccommodaties()
        {
            return iaccommodatiecontext.GetAllAccommodaties();
        }
        public List<Accommodatie> GetByFilter(string land, Season season, DateTime vertrekdatum)
        {
            return iaccommodatiecontext.GetByFilter(land, season, vertrekdatum);
        }
        public List<Accommodatie> GetByFilterSeason(Season season)
        {
            return iaccommodatiecontext.GetByFilterSeason(season);
        }
        public List<Accommodatie> GetByFilterDate(DateTime vertrekdatum)
        {
            return iaccommodatiecontext.GetByFilterDate(vertrekdatum);
        }
        public List<Accommodatie> GetByFilterSeasonLand(Season season, string land)
        {
            return iaccommodatiecontext.GetByFilterSeasonLand(season, land);
        }
        public List<Accommodatie> GetByFilterSeasonDate(Season season, DateTime vertrekdatum)
        {
            return iaccommodatiecontext.GetByFilterSeasonDate(season, vertrekdatum);
        }
        public List<Accommodatie> GetByFilterLandDate(string land, DateTime vertrekdatum)
        {
            return iaccommodatiecontext.GetByFilterLandDate(land, vertrekdatum);
        }
        public Accommodatie GetAccommodatie(string title)
        {
            return iaccommodatiecontext.GetAccommodatie(title);
        }

        public List<Accommodatie> SearchBar(string zoekTekst)
        {
            return iaccommodatiecontext.SearchBar(zoekTekst);
        }

        public List<Accommodatie> GetTop5Accommodaties()
        {
            return iaccommodatiecontext.GetTop5Accommodaties();
        }
        public string GetTekst()
        {
            return iaccommodatiecontext.GetTekst();
        }
    }
    
}
