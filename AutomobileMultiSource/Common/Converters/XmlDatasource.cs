using AutomobileMultiSource.Common.Hub;
using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AutomobileMultiSource.Common.Converters
{
    public class XmlDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        public string DatasourceName = "DatasourceXml.xml";
        public string DatasourceLocation;

        public XmlDatasource(HttpServerUtilityBase server)
        {
            this.Server = server;
            this.DatasourceLocation = server.MapPath("~/App_Data/" + DatasourceName);
        }

        public bool CanConnect()
        {
            return File.Exists(this.DatasourceLocation);
        }

        public List<Vehicule> GetVehicules()
        {
            List<Vehicule> vehicules = new List<Vehicule>();
            XmlDocument doc = new XmlDocument();
            doc.Load(DatasourceLocation);

            XmlNodeList nodeList = doc.SelectNodes("/Vehicules/Vehicule");

            foreach (XmlNode node in nodeList)
            {
                Vehicule vehicule = new Vehicule();
                foreach (XmlNode child in node.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "Brand":
                            vehicule.Brand = child.InnerText;
                            break;
                        case "Model":
                            vehicule.Model = child.InnerText;
                            break;
                        case "Registration":
                            vehicule.Registration = child.InnerText;
                            break;
                    }
                }
                vehicules.Add(vehicule);
            }

            return vehicules;
        }
        
    }
}