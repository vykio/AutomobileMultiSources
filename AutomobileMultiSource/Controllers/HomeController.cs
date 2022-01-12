using AutomobileMultiSource.Common.Converters;
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

            ViewBag.TxtToTxt = (new TextDatasource(Server)).ToText();
            ViewBag.TxtToJSON = (new TextDatasource(Server)).ToJson();
            ViewBag.TxtToXML = (new TextDatasource(Server)).ToXml();

            return View();
        }

        public ActionResult Vehicules()
        {
            ViewBag.Message = "Toutes les annonces";

            Hub hub = new Hub(Server);

            ViewBag.Vehicules = hub.GetAll();

            return View();
        }

        public ActionResult ShowFile(string name, string type)
        {

            string path = Server.MapPath("~/AppData/");
            string file_name = path + name;

            switch (type)
            {
                case "txt":
                    System.IO.File.ReadAllText(file_name);
                    break;
                default:
                    break;
            }

            return View();
        }

        public FileResult Download(string name, string type, string downloadName)
        {
            string filename = Server.MapPath("~/App_Data/" + name);

            return File(filename, type, downloadName);
        }

        public ContentResult getContent()
        {
            return Content((new TextDatasource(Server)).ToJson());
        }
    }
}