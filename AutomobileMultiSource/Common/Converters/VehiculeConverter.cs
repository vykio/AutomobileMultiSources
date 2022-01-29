using AutomobileMultiSource.Common.Interfaces;
using AutomobileMultiSource.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AutomobileMultiSource.Common.Converters
{
    public class VehiculeConverter : Converter
    {
        public string ToCsv(List<Vehicule> vehicules)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(Vehicule)).OfType<PropertyDescriptor>();

            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);

            var valueLines = vehicules.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);

            string result = "";
            foreach(string line in lines)
            {
                result += line + Environment.NewLine;
            }

            return result;
        }

        public string ToJson(List<Vehicule> vehicules)
        {
            return JsonConvert.SerializeObject(vehicules);
        }

        public string ToXml(List<Vehicule> vehicules)
        {
            XElement cust = new XElement("Vehicules",
               from vehicule in vehicules
               select new XElement("Vehicule",
                   new XElement("Brand", vehicule.Brand),
                   new XElement("Model", vehicule.Model),
                   new XElement("Registration", vehicule.Registration)
               )
           );

            return cust.ToString();
        }
    }
}