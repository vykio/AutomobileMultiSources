using AutomobileMultiSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileMultiSource.Common.Interfaces
{
    interface Bridge
    {
        List<Vehicule> GetVehicules();
    }
}
