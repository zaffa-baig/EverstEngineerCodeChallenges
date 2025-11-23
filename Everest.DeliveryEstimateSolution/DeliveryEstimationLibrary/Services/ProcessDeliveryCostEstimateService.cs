using DeliveryEstimationLibrary.Extensions;
using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Services
{
    public class ProcessDeliveryCostEstimateService : IProcessDeliveryCostEstimateService
    {
        private IProcessPackageService _processPackageService;
        public ProcessDeliveryCostEstimateService(IProcessPackageService processPackageService)
        {
            _processPackageService = processPackageService;
        }

        /// <summary>
        /// Process Delivery Cost :
        /// Calculating the every package cost along with the offer code
        /// </summary>
        /// <param name="packageDetails"></param>
        /// <returns> List of Delivery Estimate </returns>
        public List<DeliveryEstimate> ProcessDeliveryCostService(PackageDetails packageDetails)
        {
            var packages = packageDetails.packages;
            int basePrice = packageDetails.BasePrice;
            List<DeliveryEstimate> deliveryList = new List<DeliveryEstimate>();
            foreach (Package package in packages)
            {
                DeliveryEstimate delivery = new DeliveryEstimate();
                int cost = _processPackageService.ProcessSinglePackage(package, basePrice);
                //validate offer 
                bool validate = package.OfferCode.ValidCouponOffer(package.Distance, package.Weight);
                delivery.PackageId = package.PackageId;
                if (validate)
                {
                    delivery.Discount = package.OfferCode.CalculateCouponOffer(cost);
                    delivery.TotalCost = cost - delivery.Discount;
                }
                else
                {
                    delivery.TotalCost = cost;
                }
                deliveryList.Add(delivery);

            }
            return deliveryList;
        }

       
    }
}
