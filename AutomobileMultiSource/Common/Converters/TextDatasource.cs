using AutomobileMultiSource.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AutomobileMultiSource.Common.Converters
{
    public class TextDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        private string DatasourceName = "DatasourceTxt.txt";
        private string DatasourceLocation;

        public TextDatasource(HttpServerUtilityBase server)
        {
            this.Server = server;
            this.DatasourceLocation = server.MapPath("~/App_Data/" + DatasourceName);
        }

        public string ToJson()
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

        public string ToText()
        {
            return File.ReadAllText(@DatasourceLocation);
        }

        public string ToXml()
        {
            throw new NotImplementedException();
        }
    }
}