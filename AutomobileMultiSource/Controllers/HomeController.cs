using AutomobileMultiSource.Common.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileMultiSource.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Panel()
        {
            ViewBag.Message = "Vous êtes sur le prototype de la page panel.";

            return View();
        }

        public ActionResult Vehicules()
        {
            ViewBag.Message = "Toutes les annonces";

            Hub hub = new Hub(Server);

            ViewBag.Vehicules = hub.GetAll();

            return View();
        }
    }
}