using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Interface
{
    public interface IProcessDeliveryCostEstimateService
    {
        public List<DeliveryEstimate> ProcessDeliveryCostService(PackageDetails packageDetails);
    }
}
