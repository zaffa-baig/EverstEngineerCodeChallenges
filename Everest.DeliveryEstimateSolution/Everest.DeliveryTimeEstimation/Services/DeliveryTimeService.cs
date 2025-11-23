using DeliveryEstimationLibrary.Extensions;
using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using Everest.DeliveryCostEstimation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.DeliveryTimeEstimation.Services
{
    public class DeliveryTimeService : IDeliveryTimeService
    {
        private IProcessDeliveryTimeEstimateService _processDeliveryTimeEstimateService;

        public DeliveryTimeService(IProcessDeliveryTimeEstimateService processDeliveryTimeEstimateService)
        {
            _processDeliveryTimeEstimateService = processDeliveryTimeEstimateService;
        }
        /// <summary>
        /// Process delivery Time main method 
        /// </summary>
        /// <param name="packageDetails"></param>
        /// <returns>List of result as output</returns>
        public List<string> ProcessDeliveryTime(PackageDetails packageDetails)
        {
            List<DeliveryEstimate> deliveryList = [];
            List<string> result = [];

            try
            {
                deliveryList = _processDeliveryTimeEstimateService.ProcessDeliveryTimeService(packageDetails);
                foreach (var delivery in deliveryList)
                {
                    string deliveryCostWithDiscount = $"{delivery.PackageId} {delivery.Discount} {delivery.TotalCost} {delivery.TravelTime}";
                    result.Add(deliveryCostWithDiscount);

                }

            }
            catch (Exception ex)
            {
                Console.Write("Error occur while processing indivisual package with calculating Time Taken. Error info :" + ex.Message);

            }

            return result;
        }

    }
}
