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

        private string DatasourceName = "DatasourceTxt.txt";
        private string DatasourceLocation;

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
            return JsonToVehicule.GetVehicules(this.ToJson());
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

        public string[] ToTextLines()
        {
            return File.ReadAllLines(@DatasourceLocation);
        }


        public string ToXml()
        {
            String[] source = File.ReadAllLines(@DatasourceLocation);
            source = source.Skip(1).ToArray();

            XElement cust = new XElement("Root",
                from str in source
                let fields = str.Split('\t')
                select new XElement("Vehicule",
                    //new XAttribute("Brand", fields[0]),
                    new XElement("Brand", fields[0]),
                    new XElement("Model", fields[1]),
                    new XElement("Registration", fields[2])
                    /*new XElement("ContactTitle", fields[3]),
                    new XElement("Phone", fields[4]),
                    new XElement("FullAddress",
                        new XElement("Address", fields[5]),
                        new XElement("City", fields[6]),
                        new XElement("Region", fields[7]),
                        new XElement("PostalCode", fields[8]),
                        new XElement("Country", fields[9])
                    )*/
                )
            );

            return cust.ToString();
        }

        public void toSQL()
        {
           string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename = " + this.DatasourceLocation + ";Integrated Security = True";

           SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            /* a tester
                BULK
                INSERT MyTable
                FROM 'c:\myfile.txt'
                WITH
                (
                FIELDTERMINATOR = ',',
                ROWTERMINATOR = '\n')
            */

        }

        public void toCsv()
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@DatasourceLocation);

            foreach (string line in lines)
            {
                csv.Add(line.Split('\t'));
            }

            string writePath = Server.MapPath("~/Generated_Data/")+ "TEST.csv";
                
            File.WriteAllLines(writePath, csv.Select(x => string.Join(",", x)));

        }
    }
}