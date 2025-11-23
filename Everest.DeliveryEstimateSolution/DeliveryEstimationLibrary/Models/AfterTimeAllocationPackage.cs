using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Models
{
    public class AfterTimeAllocationPackage
    {
        public string PackageId { get; set; } = string.Empty;
        public decimal TimeTaken { get; set; }

    }
}
