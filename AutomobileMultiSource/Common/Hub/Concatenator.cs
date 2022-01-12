using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Hub
{
    public class Concatenator
    {

        public static string Json(List<String> jsons)
        {

            List<Vehicule> vehicules = new List<Vehicule>();

            foreach(string json in jsons)
            {
                vehicules.AddRange(JsonToVehicule.GetVehicules(json));
            }

            return JsonToVehicule.GetJson(vehicules);

        }

    }
}