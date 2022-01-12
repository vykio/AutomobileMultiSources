using AutomobileMultiSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Hub
{
    public class JsonToVehicule
    {

        public static List<Vehicule> GetVehicules(string json)
        {
            return JsonConvert.DeserializeObject<List<Vehicule>>(json);
        }

        public static string GetJson(List<Vehicule> vehicules)
        {
            return JsonConvert.SerializeObject(vehicules);
        } 

    }
}