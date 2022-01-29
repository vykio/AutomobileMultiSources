using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileMultiSource.Common.Interfaces
{
    interface Converter
    {
        string ToJson(List<Vehicule> vehicules);
        string ToXml(List<Vehicule> vehicules);
        string ToCsv(List<Vehicule> vehicules);
    }
}
