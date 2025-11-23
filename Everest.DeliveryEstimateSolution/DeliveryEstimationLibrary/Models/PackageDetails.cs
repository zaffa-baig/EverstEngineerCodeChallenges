using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Models
{
    public class PackageDetails
    {
        public int BasePrice { get; set; }
        public int PackageCount { get; set; }
        public List<Package> packages { get; set; } = new List<Package>();
        public int NoOfVehicles { get; set; }
        public int Speed { get; set; }
        public int LimitedWeight { get; set; }
    }
}
