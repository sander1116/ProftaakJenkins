using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F_Reizen.Models;
using Microsoft.Ajax.Utilities;
using PresentationLayer.ViewModels;
using RepositoryPattern.Repository;
using RepositoryPattern.Factory;

namespace PresentationLayer.Controllers
{
  public class AccommodationController : Controller
  {
    public AccommodatieRepository accommodatieRepository = new AccommodatieRepository(AccommodatieFactory.Get(1));
    public HomeRepository homeRepository = new HomeRepository(HomeFactory.Get(1));

    public ZoekresultatenViewModel zoekresultatenViewModel;


    // GET: Accommodation
    public ActionResult Index()
    {
      return View();
    }

        public ActionResult AccommodatiePagina(string title)
        {
            AccommodatieViewModel accommodatieViewModel;
            if (Session["Reisdata"] != null)
            {
                accommodatieViewModel = (AccommodatieViewModel)Session["Reisdata"];
                accommodatieViewModel.Accommodatie = accommodatieRepository.GetAccommodatie(title);
            }
            else
            {
                accommodatieViewModel = new AccommodatieViewModel
                {
                    Accommodatie = accommodatieRepository.GetAccommodatie(title)
                };
            }
            
            Session["Accommodatie"] = accommodatieViewModel.Accommodatie;
            return View("Accommodatie", accommodatieViewModel);
        }

    public ActionResult Zoekresultaten(HomeViewModel homeViewModel)
    {
      zoekresultatenViewModel = new ZoekresultatenViewModel();
      zoekresultatenViewModel.landen = homeRepository.GetLanden();
      zoekresultatenViewModel.season = homeViewModel.season;
      zoekresultatenViewModel.land = homeViewModel.land;
      zoekresultatenViewModel.date = homeViewModel.date;
      zoekresultatenViewModel.gefilterdAccommodaties = zoeken(zoekresultatenViewModel).gefilterdAccommodaties;
      return View(zoekresultatenViewModel);
    }
    
    public ZoekresultatenViewModel zoeken(ZoekresultatenViewModel zoekresultatenViewModel)
    {
      //geen filter
      if (zoekresultatenViewModel.season == Season.Nothing && zoekresultatenViewModel.land == null && zoekresultatenViewModel.date.ToString() == "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetAllAccommodaties();
      }
      //alleen seizoen
      else if (zoekresultatenViewModel.season != Season.Nothing && zoekresultatenViewModel.land == null && zoekresultatenViewModel.date.ToString() == "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterSeason(zoekresultatenViewModel.season);
      }
      //alleen land
      else if (zoekresultatenViewModel.season == Season.Nothing && zoekresultatenViewModel.land != null && zoekresultatenViewModel.date.ToString() == "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterLand(zoekresultatenViewModel.land);
      }
      //alleen datum
      else if (zoekresultatenViewModel.season == Season.Nothing && zoekresultatenViewModel.land == null && zoekresultatenViewModel.date.ToString() != "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterDate(zoekresultatenViewModel.date);
      }
      //seizoen en land
      else if (zoekresultatenViewModel.season != Season.Nothing && zoekresultatenViewModel.land != null && zoekresultatenViewModel.date.ToString() == "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterSeasonLand(zoekresultatenViewModel.season, zoekresultatenViewModel.land);
      }
      //seizoen en datum
      else if (zoekresultatenViewModel.season != Season.Nothing && zoekresultatenViewModel.land == null && zoekresultatenViewModel.date.ToString() != "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterSeasonDate(zoekresultatenViewModel.season, zoekresultatenViewModel.date);
      }
      //datum en land
      else if (zoekresultatenViewModel.season == Season.Nothing && zoekresultatenViewModel.land != null && zoekresultatenViewModel.date.ToString() != "1-1-0001 00:00:00")
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterLandDate(zoekresultatenViewModel.land, zoekresultatenViewModel.date);
      }
      //alles
      else
      {
        zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilter(zoekresultatenViewModel.land, zoekresultatenViewModel.season, zoekresultatenViewModel.date);
      }
      return zoekresultatenViewModel;
    }

    public ActionResult InspiratieVakanties(int season)
    {
      zoekresultatenViewModel = new ZoekresultatenViewModel();
      zoekresultatenViewModel.landen = homeRepository.GetLanden();
      zoekresultatenViewModel.season = (Season)season;
      zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.GetByFilterSeason(zoekresultatenViewModel.season);
      return View("Zoekresultaten", zoekresultatenViewModel);
    }

        public ActionResult boekenZoekresultaten(string datum, string title)
        {
            AccommodatieViewModel viewModel = new AccommodatieViewModel(); ;
            if (Session["Reisdata"] != null)
            {
                viewModel = (AccommodatieViewModel)Session["Reisdata"];
            }
            viewModel.Datum = Convert.ToDateTime(datum);
            viewModel.Accommodatie = accommodatieRepository.GetAccommodatie(title);
            Session["Accommodatie"] = viewModel.Accommodatie;
            Session["Reisdata"] = viewModel;
            return RedirectToAction("Index", "Boeking");
        }

        public ActionResult boekenAccommodatie(string kamer, string kinderen, string datum, string titel)
        {
            AccommodatieViewModel viewModel = new AccommodatieViewModel();
            viewModel.AantalKinderen = Convert.ToInt32(kinderen);
            viewModel.AantalKamers = Convert.ToInt32(kamer);
            viewModel.Datum = Convert.ToDateTime(datum);
            viewModel.Accommodatie = accommodatieRepository.GetAccommodatie(titel);
            Session["Accommodatie"] = viewModel.Accommodatie;
            Session["Reisdata"] = viewModel;
            return Json(Url.Action("Index", "Boeking"));
        }

        public ActionResult priceDate(string titel, string datum2, string kinderen2, string kamers2)
        {
            Accommodatie accommodatie = accommodatieRepository.GetAccommodatie(titel);
            DateTime datum = Convert.ToDateTime(datum2);
            int kinderen = Convert.ToInt32(kinderen2);
            int kamers = Convert.ToInt32(kamers2);

            return Json(new { price = PrijsBerekening(accommodatie, datum, kinderen, kamers) });
        }

        public ActionResult pageLoadPrice()
        {
            AccommodatieViewModel avm;
            if (Session["Reisdata"] != null)
            {
                avm = (AccommodatieViewModel)Session["Reisdata"];
                if (avm.Datum.Year < 2000)
                {
                    avm.Datum = new DateTime(2018, (int)Month.March, 1);
                }
            }
            else
            {
                avm = new AccommodatieViewModel()
                {
                    Accommodatie = (Accommodatie)Session["Accommodatie"],
                    Datum = new DateTime(2018, 3, 1),
                    AantalKinderen = 0,
                    AantalKamers = 1
                };
            }
            return Json(new { price = PrijsBerekening(avm.Accommodatie, avm.Datum, avm.AantalKinderen, avm.AantalKamers) });
        }

        public decimal PrijsBerekening(Accommodatie accommodatie, DateTime datum, int kinderen, int kamers)
        {
            AccommodatieViewModel accommodatieViewModel = new AccommodatieViewModel()
            {
                Accommodatie = accommodatie,
                Datum = datum,
                AantalKinderen = kinderen,
                AantalKamers = kamers
            };
            Session["Reisdata"] = accommodatieViewModel;

            decimal ChildrenPrice;
            decimal AdultPrice;
            int toeslag = 0;
            int[] months = new int[12];
            months[0] = 120;
            months[1] = 110;
            months[2] = 100;
            months[3] = 110;
            months[4] = 120;
            months[5] = 130;
            months[6] = 120;
            months[7] = 110;
            months[8] = 100;
            months[9] = 110;
            months[10] = 120;
            months[11] = 130;

      System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
      string monthname = mfi.GetMonthName(datum.Month).ToString();
      Month month = (Month)Enum.Parse(typeof(Month), monthname);
      AdultPrice = (accommodatie.Volwasseneprijs * 2) * months[(int)month] / 100;
      ChildrenPrice = (accommodatie.Kinderprijs * kinderen) * months[(int)month] / 100;
      if (kamers == 3)
      {
        toeslag = 50;
      }
      return AdultPrice + ChildrenPrice + toeslag + 20.50m;
    }

    public ActionResult reisGezelschap(string titel, int kinderen2, string kamers2, string datum2)
    {
      Accommodatie accommodatie = accommodatieRepository.GetAccommodatie(titel);
      DateTime datum = Convert.ToDateTime(datum2);
      int kinderen = Convert.ToInt32(kinderen2);
      int kamers = Convert.ToInt32(kamers2);
      return Json(new { price = PrijsBerekening(accommodatie, datum, kinderen, kamers) });
    }

        public ActionResult SearchBar()
        {
            string searchText = Request.Form["Searchbar"].ToLower();
            
                zoekresultatenViewModel = new ZoekresultatenViewModel();
                zoekresultatenViewModel.landen = homeRepository.GetLanden();
                zoekresultatenViewModel.gefilterdAccommodaties = accommodatieRepository.SearchBar(searchText);
            
            return View("Zoekresultaten", zoekresultatenViewModel);
        }

        public ActionResult DatumBoeking(string datum, string accommodatieTitel)
        {
            AccommodatieViewModel viewModel = new AccommodatieViewModel(); ;
            if (Session["Reisdata"] != null)
            {
                viewModel = (AccommodatieViewModel)Session["Reisdata"];
            }
            viewModel.Datum = Convert.ToDateTime(datum);
            Session["Accommodatie"] = accommodatieRepository.GetAccommodatie(accommodatieTitel);
            Session["Reisdata"] = viewModel;
            return RedirectToAction("Index", "Boeking");
        }
    }
}