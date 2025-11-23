using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Models
{
    public class DeliveryEstimate
    {
        public string PackageId { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
        public decimal Discount { get; set; } = 0;
        public decimal TravelTime { get; set; }
    }
}
