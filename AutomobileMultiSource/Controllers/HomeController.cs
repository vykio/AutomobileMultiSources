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

            switch (type)
            {
                case "application/txt":
                    string text = textDatasource.ToText();
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

        public ContentResult getContent()
        {
            return Content((new TextDatasource(Server)).ToJson());
        }

        public FileResult ConvertFileFromTxt(string name, string sourceType, string newType, string downloadName)
        {
            if(sourceType == "application/txt")
            {
                string filename = Server.MapPath("~/App_Data/" + name);
                string downloadFilePath = Server.MapPath("~/Generated_Data/" + downloadName);
                TextDatasource textDatasource = new TextDatasource(Server);

                switch (newType)
                {
                    case "application/xml":
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(textDatasource.ToXml());
                        StreamWriter outStream = System.IO.File.CreateText(downloadFilePath);
                        doc.Save(outStream);
                        outStream.Close();
                        break;
                    case "application/json":
                        string json = textDatasource.ToJson();
                        System.IO.File.WriteAllText(downloadFilePath, json);
                        break;
                    case "application/sql":
                        
                        break;
                    case "application/csv":
                        textDatasource.toCsv();
                        break;
                    default:
                        break;
                }

                return File(downloadFilePath, newType, downloadName);
            }

            return null;
        }

        public FileResult ConvertFileFromMdf(string name, string sourceType, string newType, string downloadName)
        {
            if (sourceType == "application/sql")
            {
                string filename = Server.MapPath("~/App_Data/" + name);
                string downloadFilePath = Server.MapPath("~/Generated_Data/" + downloadName);
                SqlDatasource sqlDatasource = new SqlDatasource(Server);

                switch (newType)
                {
                    case "application/xml":
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(sqlDatasource.ToXml());
                        StreamWriter outStream = System.IO.File.CreateText(downloadFilePath);
                        doc.Save(outStream);
                        outStream.Close();
                        break;
                    case "application/json":
                        string json = sqlDatasource.ToJson();
                        System.IO.File.WriteAllText(downloadFilePath, json);
                        break;
                    case "application/csv":
                        break;
                    default:
                        break;
                }

                //var byteArray = Encoding.ASCII.GetBytes(string_with_your_data);
                //var stream = new MemoryStream(byteArray);


                return File(downloadFilePath, newType, downloadName);
            }

            return null;
        }

        public FileResult ConvertFileFromXml(string name, string sourceType, string newType, string downloadName)
        {
            if (sourceType == "application/xml")
            {
                string filename = Server.MapPath("~/App_Data/" + name);
                string downloadFilePath = Server.MapPath("~/Generated_Data/" + downloadName);
                XmlDatasource xmlDatasource = new XmlDatasource(Server);

                switch (newType)
                {
                    case "application/txt":
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlDatasource.ToText());
                        StreamWriter outStream = System.IO.File.CreateText(downloadFilePath);
                        doc.Save(outStream);
                        outStream.Close();
                        break;
                    case "application/json":
                        string json = xmlDatasource.ToJson();
                        System.IO.File.WriteAllText(downloadFilePath, json);
                        break;
                    default:
                        break;
                }

                return File(downloadFilePath, newType, downloadName);
            }

            return null;
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