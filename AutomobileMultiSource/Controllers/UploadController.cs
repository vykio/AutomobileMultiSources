using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileMultiSource.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, String datasourceType)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    String _FileName;
                    switch(datasourceType)
                    {
                        case "txt":
                            _FileName = "DatasourceTxt.txt";
                            break;
                        case "xml":
                            _FileName = "DatasourceXml.xml";
                            break;
                        case "json":
                            _FileName = "DatasourceJson.json";
                            break;
                        case "database":
                            _FileName = "FinalDatabase.mdf";
                            break;
                        case "sql":
                        default:
                            _FileName = "DatasourceSQL.mdf";
                            break;
                    }
                    string _path = Path.Combine(Server.MapPath("~/App_Data"), _FileName);
                    file.SaveAs(_path);
                }
                switch(datasourceType)
                {
                    case "txt":
                        ViewBag.UploadTxtMessage = "La source de données Texte à été uploadée vers /App_Data/DatasourceTxt.txt";
                        break;
                    case "sql":
                        ViewBag.UploadSqlMessage = "La source de données SQL à été uploadée vers /App_Data/DatasourceSQL.mdf";
                        break;
                    case "json":
                        ViewBag.UploadJsonMessage = "La source de données JSON à été uploadée vers /App_Data/DatasourceJson.json";
                        break;
                    case "xml":
                        ViewBag.UploadXmlMessage = "La source de données XML à été uploadée vers /App_Data/DatasourceXml.xml";
                        break;
                    case "database":
                        ViewBag.UploadTargetMessage = "La base de données cible a été importée";
                        break;
                }
                
                return View();
            }
            catch
            {
                switch (datasourceType)
                {
                    case "txt":
                        ViewBag.UploadTxtMessage = "La source de données Texte n'a pas pu être uploadée, vérifiez l'existance du dossier App_Data/ dans le projet";
                        break;
                    case "sql":
                        ViewBag.UploadSqlMessage = "La source de données SQL n'a pas pu être uploadée, vérifiez l'existance du dossier App_Data/ dans le projet";
                        break;
                    case "json":
                        ViewBag.UploadJsonMessage = "La source de données JSON n'a pas pu être uploadée, vérifiez l'existance du dossier App_Data/ dans le projet";
                        break;
                    case "xml":
                        ViewBag.UploadXmlMessage = "La source de données XML n'a pas pu être uploadée, vérifiez l'existance du dossier App_Data/ dans le projet";
                        break;
                    case "database":
                        ViewBag.UploadTargetMessage = "La base de données cible n'a pas pu être uploadée, vérifiez l'existance du dossier App_Data/ dans le projet";
                        break;
                }
                return View();
            }
        }
    }
}