using AutomobileMultiSource.Common.Converters;
using AutomobileMultiSource.Common.Hub;
using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

            return View();
        }

        public ActionResult Panel()
        {
            TargetDatasource target = new TargetDatasource(Server);
            ViewBag.Target = target.CanConnect() ? target.GetVehicules() : null;

            SqlDatasource sqlSource = new SqlDatasource(Server);
            ViewBag.Sql = sqlSource.CanConnect() ? sqlSource.GetVehicules() : new List<Vehicule>();

            TextDatasource textSource = new TextDatasource(Server);
            ViewBag.Text = textSource.CanConnect() ? textSource.GetVehicules() : new List<Vehicule>();

            JsonDatasource jsonSource = new JsonDatasource(Server);
            ViewBag.Json = jsonSource.CanConnect() ? jsonSource.GetVehicules() : new List<Vehicule>();

            XmlDatasource xmlSource = new XmlDatasource(Server);
            ViewBag.Xml = xmlSource.CanConnect() ? xmlSource.GetVehicules() : new List<Vehicule>();

            return View();
        }

        public ActionResult Vehicules()
        {
            ViewBag.Message = "Toutes les annonces";

            Hub hub = new Hub(Server);

            ViewBag.Vehicules = hub.targetConnected() ? hub.GetFromTarget() : new List<Vehicule>();

            return View();
        }

        public ActionResult ShowFile(string name, string type)
        {

            string path = Server.MapPath("~/App_Data/");
            string file_name = path + name;
            TextDatasource textDatasource = new TextDatasource(Server);

            VehiculeConverter converter = new VehiculeConverter();

            switch (type)
            {
                case "application/txt":
                    string text = converter.ToCsv(textDatasource.GetVehicules());
                    using (StreamReader sr = new StreamReader(file_name))
                    {
                        while (!sr.EndOfStream)
                        {
                            text += sr.ReadLine() + Environment.NewLine;
                        }
                    }
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

        public FileResult ConvertFile(string sourceName, string sourceType, string newType, string downloadName)
        {
            dynamic datasource;
            switch(sourceType)
            {
                case "txt":
                    datasource = new TextDatasource(Server);
                    break;
                case "xml":
                    datasource = new XmlDatasource(Server);
                    break;
                case "json":
                    datasource = new JsonDatasource(Server);
                    break;
                case "database":
                    datasource = new TargetDatasource(Server);
                    break;
                case "mdf":
                default:
                    datasource = new SqlDatasource(Server);
                    break;
            }

            VehiculeConverter converter = new VehiculeConverter();
            string DownloadFilePath = Server.MapPath("~/Generated_Data/" + downloadName);

            if (System.IO.File.Exists(DownloadFilePath))
            {
                System.IO.File.Delete(DownloadFilePath);
            }

            string output = "";
            switch (newType)
            {
                case "application/xml":
                    output = converter.ToXml(datasource.GetVehicules());
                    break;
                case "application/json":
                    output = converter.ToJson(datasource.GetVehicules());
                    break;
                case "application/csv":
                    output = converter.ToCsv(datasource.GetVehicules());
                    break;
                default:
                    break;
            }

            using (StreamWriter writer = System.IO.File.CreateText(DownloadFilePath))
            {
                writer.WriteLine(output);
            }

            return File(DownloadFilePath, newType, downloadName);

        }


        public RedirectResult DeleteDatabaseContent()
        {
            Hub hub = new Hub(Server);
            ViewBag.Target = hub.DeleteTarget();

            return Redirect("/Home/Panel");
        }

        public RedirectResult FillDatabaseContent()
        {
            Hub hub = new Hub(Server);
            ViewBag.Target = hub.FillTarget();

            return Redirect("/Home/Panel");
        }

    }
}