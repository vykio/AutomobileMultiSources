using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Hub
{
    public class Concatenator
    {

        public static List<Vehicule> Concatenate(List<List<Vehicule>> listes)
        {

            List<Vehicule> vehicules = new List<Vehicule>();

            foreach(List<Vehicule> listeVehicule in listes)
            {
                vehicules.AddRange(listeVehicule);
            }

            return vehicules;

        }

    }
}