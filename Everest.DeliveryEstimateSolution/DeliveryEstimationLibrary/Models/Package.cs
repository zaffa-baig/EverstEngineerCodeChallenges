using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Models
{
    public class Package
    {
        public string PackageId { get; set; } = string.Empty;
        public int Weight { get; set; } = 0;
        public int Distance { get; set; } = 0;
        public string OfferCode { get; set; } = string.Empty;
    }
}
