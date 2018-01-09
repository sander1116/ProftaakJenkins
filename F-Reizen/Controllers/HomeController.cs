using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F_Reizen.Models;
using PresentationLayer.ViewModels;
using RepositoryPattern.Repository;
using RepositoryPattern.Context;
using RepositoryPattern.Factory;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public HomeRepository homeRepository = new HomeRepository(HomeFactory.Get(1));
        public AccommodatieRepository accommodatieRepository = new AccommodatieRepository(AccommodatieFactory.Get(1));
        public HomeViewModel homeViewModel;

        public ActionResult Index()
        {
            homeViewModel = new HomeViewModel();
            homeViewModel.landen = homeRepository.GetLanden();
            List<Accommodatie> accommodaties = accommodatieRepository.GetTop5Accommodaties();
            homeViewModel.accommodaties = new List<Accommodatie>();
            homeViewModel.accommodaties.Add(accommodaties[0]);
            homeViewModel.accommodaties.Add(accommodaties[1]);
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            PresentationLayer.ViewModels.AboutViewModel about = new PresentationLayer.ViewModels.AboutViewModel();
            return View(about);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public bool PlaceBooking(Boeking booking, Transport transportation)
        {
            return true;
        }

        public ActionResult Login()
        {
            return RedirectToAction("Index");
        }
    }
}