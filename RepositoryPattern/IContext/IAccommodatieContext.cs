using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Reizen.Models;
using RepositoryPattern.IContext;


namespace RepositoryPattern.IContext
{
    public interface IAccommodatieContext
    {
        List<Accommodatie> GetByFilter(string land, Season season, DateTime vertrekdatum);
        List<Accommodatie> GetAllAccommodaties();
        string GetTekst();
        List<Accommodatie> GetByFilterSeason(Season season);
        List<Accommodatie> GetByFilterDate(DateTime vertrekdatum);
        List<Accommodatie> GetByFilterSeasonLand(Season season, string land);
        List<Accommodatie> GetByFilterSeasonDate(Season season, DateTime vertrekdatum);
        List<Accommodatie> GetByFilterLandDate(string land, DateTime vertrekdatum);
        List<Accommodatie> GetTop5Accommodaties();
        Accommodatie GetAccommodatie(string title);
        List<Accommodatie> GetByFilterLand(string searchText);
        List<Accommodatie> SearchBar(string zoekTekst);
    }
}
