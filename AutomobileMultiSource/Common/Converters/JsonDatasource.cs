using AutomobileMultiSource.Common.Hub;
using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Converters
{
    public class JsonDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        public string DatasourceName = "DatasourceJson.json";
        public string DatasourceLocation;

        public JsonDatasource(HttpServerUtilityBase server)
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
            return JsonToVehicule.GetVehicules(this.GetData());
        }

        public string GetData()
        {
            return File.ReadAllText(DatasourceLocation);
        }

        /*public string ToText()
        {
            throw new NotImplementedException();
        }

        public string ToXml()
        {
            throw new NotImplementedException();
        }*/
    }
}