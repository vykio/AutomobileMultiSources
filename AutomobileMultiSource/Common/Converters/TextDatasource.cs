using AutomobileMultiSource.Common.Hub;
using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutomobileMultiSource.Common.Converters
{
    public class TextDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        public string DatasourceName = "DatasourceTxt.txt";
        public string DatasourceLocation;

        public TextDatasource(HttpServerUtilityBase server)
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

        private string GetData()
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@DatasourceLocation);

            foreach (string line in lines)
            {
                csv.Add(line.Split('\t'));
            }

            var properties = lines[0].Split('\t');
            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                {
                    objResult.Add(properties[j], csv[i][j]);
                }
                listObjResult.Add(objResult);
            }

            return JsonConvert.SerializeObject(listObjResult);
        }

        
    }
}