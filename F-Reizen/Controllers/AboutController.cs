using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            AboutViewModel about = new AboutViewModel();
            return View(about);
        }
    }
}