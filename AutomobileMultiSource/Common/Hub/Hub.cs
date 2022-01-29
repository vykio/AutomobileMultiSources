using AutomobileMultiSource.Common.Converters;
using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Hub
{
    public class Hub
    {

        private HttpServerUtilityBase Server;

        List<Object> sources;
        TargetDatasource target;

        public bool targetConnected()
        {
            return this.target.CanConnect();
        }

        public Hub(HttpServerUtilityBase server)
        {
            this.Server = server;

            /* Liste des datasources */
            sources = new List<Object>();
            sources.Add(new TextDatasource(this.Server));
            sources.Add(new SqlDatasource(this.Server));
            sources.Add(new JsonDatasource(this.Server));
            sources.Add(new XmlDatasource(this.Server));

            target = new TargetDatasource(this.Server);
        }

        public List<Vehicule> GetFromTarget()
        {
            return this.target.GetVehicules();
        }

        public List<Vehicule> DeleteTarget()
        {
            return this.target.DeleteAllContent();
        }

        public List<Vehicule> FillTarget()
        {
            return this.target.Fill(this.GetAll());
        }

        public List<Vehicule> GetAll()
        {
            List<Vehicule> liste = new List<Vehicule>();
            foreach (dynamic source in this.sources)
            {
                liste.AddRange(source.GetVehicules());
            }

            return liste;
        }

    }
}