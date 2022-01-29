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

namespace AutomobileMultiSource.Common.Converters
{
    public class XmlDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        private string DatasourceName = "DatasourceXml.xml";
        private string DatasourceLocation;

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
            return JsonToVehicule.GetVehicules(this.ToJson());
        }

        public string ToJson()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(DatasourceLocation));
            return JsonConvert.SerializeXmlNode(doc); ;
        }

        public string ToText()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(DatasourceLocation));

            StringBuilder sb = new StringBuilder(1024);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (!(string.IsNullOrEmpty(node.Name) || string.IsNullOrEmpty(node.InnerText)))
                {
                    string nodeName = node.Name;
                    sb.Append(char.ToUpper(node.Name[0]));
                    sb.Append(node.Name.Substring(1));
                    sb.Append(": ");
                    sb.AppendLine(node.InnerText);
                    sb.AppendLine();

                }
            }

            return sb.ToString();

        }

        public string ToXml()
        {
            return File.ReadAllText(@DatasourceLocation);
        }
    }
}