using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Outsourcing_F_Reizen.Database;
using Outsourcing_F_Reizen.Models;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index(string returnurl)
        {
            TempData["returnurl"] = returnurl;
            return View();
        }

        public ActionResult Inloggen(string username, string password, string returnurl)
        {
            Account loginUser = null;
            //HttpContext.User.Identity.Name
            Account account = new Account(username, password);
            try
            {
                loginUser = DB.LoginCheck(account);

            }
            catch (SqlException e)
            {

            }

            if (loginUser != null)
            {
                FormsAuthentication.SetAuthCookie(loginUser.Username, false);
                if (loginUser is Account)
                {
                    if (!string.IsNullOrEmpty(returnurl))
                    {
                        return Redirect(returnurl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginCheck = "U heeft de verkeerde combinatie van gebruikersnaam en wachtwoord ingevuld!";
            }
            return View("Index");

        }

        public ActionResult LogUit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}