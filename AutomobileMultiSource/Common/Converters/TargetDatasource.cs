using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Converters
{
    public class TargetDatasource
    {

        private HttpServerUtilityBase Server;

        private string DatasourceName = "FinalDatabase.mdf";
        private string DatasourceLocation;
        private string connectionString;

        public TargetDatasource(HttpServerUtilityBase server)
        {
            this.Server = server;
            this.DatasourceLocation = server.MapPath("~/App_Data/" + DatasourceName);
            this.connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename = " + this.DatasourceLocation + ";Integrated Security = True";
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

                foreach (DataRow dr in dt.Rows)
                {
                    vehicules.Add(new Vehicule
                    {
                        Brand = dr[0].ToString().Trim(),
                        Model = dr[1].ToString().Trim(),
                        Registration = dr[2].ToString().Trim()
                    });
                }
            }

            return vehicules;
        }

        public List<Vehicule> DeleteAllContent()
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            connection.Open();

            string Request = "TRUNCATE TABLE [Vehicule]";

            SqlCommand cmd = new SqlCommand(Request, connection);
            cmd.ExecuteNonQuery();

            connection.Close();

            return this.GetVehicules();
        }

        public List<Vehicule> Fill(List<Vehicule> vehicules)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            connection.Open();
            

            foreach(Vehicule vehicule in vehicules)
            {
                string Request = "INSERT INTO [Vehicule] (Brand, Model, Registration) VALUES (@Brand, @Model, @Registration)";

                SqlCommand cmd = new SqlCommand(Request, connection);
                
                cmd.Parameters.AddWithValue("@Brand", vehicule.Brand);
                cmd.Parameters.AddWithValue("@Model", vehicule.Model);
                cmd.Parameters.AddWithValue("@Registration", vehicule.Registration);

                cmd.ExecuteNonQuery();
                
            }

            connection.Close();

            return this.GetVehicules();

        }
       

    }
}