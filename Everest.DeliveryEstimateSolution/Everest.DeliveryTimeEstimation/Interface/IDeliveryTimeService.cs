using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.DeliveryCostEstimation.Interface
{
    interface IDeliveryTimeService
    {
        List<string> ProcessDeliveryTime(PackageDetails packageDetails);
    }
}
