﻿using AutomobileMultiSource.Common.Converters;
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

            /* Je pense que ici, l'idée est de concaténer les différentes sources de données dans un certain format
             * Il faut donc coder une sorte de Concatenator avec plusieurs fonctions pour les différentes sources de données.
             * 
             * On peut lui donner soit toutes les données en texte, soit toutes les données en XML etc. et il nous sort un texte
             * dans le format choisi (égale au format des données) où tout est concaténé. 
             * 
             * A réfléchir...
             */

            string json = this.GetJson();

            return JsonToVehicule.GetVehicules(json);

            /* Pourquoi pas retourner une liste de <Models/Vehicule.cs> */

            throw new NotImplementedException();
        }

        public string GetJson()
        {
            // Retourner la concaténation de toutes les sources de données
            List<String> jsons = new List<String>();

            foreach(dynamic source in this.sources)
            {
                jsons.Add(source.ToJson());
            }
            
            return Concatenator.Json(jsons);
        }

        public string GetText()
        {
            // Retourner la concaténation de toutes les sources de données
            throw new NotImplementedException();
        }

        public string GetXml()
        {
            // Retourner la concaténation de toutes les sources de données
            throw new NotImplementedException();
        }

    }
}