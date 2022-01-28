using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutomobileMultiSource.Common.Converters
{
    public class SqlDatasource : Bridge
    {

        private HttpServerUtilityBase Server;

        private string DatasourceName = "DatasourceSQL.mdf";
        private string DatasourceLocation;
        private string connectionString;

        public SqlDatasource(HttpServerUtilityBase server)
        {
            this.Server = server;
            this.DatasourceLocation = server.MapPath("~/App_Data/" + DatasourceName);
            this.connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename = " +this.DatasourceLocation + ";Integrated Security = True";
        }

        public bool CanConnect()
        {
            return File.Exists(this.DatasourceLocation);
        }

        public List<Vehicule> GetVehicules()
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            List<Vehicule> vehicules = new List<Vehicule>();

            connection.Open();

            string Request = "SELECT [Brand], [Model], [Registration] FROM [Vehicule]";

            using (SqlDataAdapter adapter = new SqlDataAdapter(Request, connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach(DataRow dr in dt.Rows)
                {
                    vehicules.Add(new Vehicule
                    {
                        Brand = dr[0].ToString().Trim(),
                        Model = dr[1].ToString().Trim(),
                        Registration = dr[2].ToString().Trim()
                    });
                }
            }

            connection.Close();

            return vehicules;
        }

        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this.GetVehicules());

            return json;
        }

        public string ToText()
        {
            throw new NotImplementedException();
        }

        public string ToXml()
        {
            string xmlString = null;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Vehicule>));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, this.GetVehicules());
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            XElement element = XElement.Parse(xmlString);

            return element.ToString();
        }

       
    }
}