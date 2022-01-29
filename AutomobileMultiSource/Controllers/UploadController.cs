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
                        ViewBag.UploadTxtMessage = "Txt Datasource Uploaded Successfully!!";
                        break;
                    case "sql":
                        ViewBag.UploadSqlMessage = "Sql Datasource Uploaded Successfully!!";
                        break;
                    case "json":
                        ViewBag.UploadJsonMessage = "Json Datasource Uploaded Successfully!!";
                        break;
                    case "xml":
                        ViewBag.UploadXmlMessage = "Xml Datasource Uploaded Successfully!!";
                        break;
                }
                
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}