using DeliveryEstimationLibrary.Constants;
using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Services
{

    public class ProcessPackageService : IProcessPackageService
    {
        /// <summary>
        /// Process indivisual package to get the delivery cost
        /// </summary>
        /// <param name="package"></param>
        /// <param name="basePrice"></param>
        /// <returns>Cost to product</returns>
        public int ProcessSinglePackage(Package package, int basePrice)
        {
            int deliveryCost = 0;

            if (package != null)
            {

                deliveryCost = basePrice + StandardConstant.WeightCost * package.Weight + StandardConstant.DistanceCost * package.Distance;
            }
            return deliveryCost;
        }
    }
}
