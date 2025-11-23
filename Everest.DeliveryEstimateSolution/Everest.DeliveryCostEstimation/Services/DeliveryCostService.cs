using DeliveryEstimationLibrary.Extensions;
using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using Everest.DeliveryCostEstimation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.DeliveryCostEstimation.Services
{
    public class DeliveryCostService : IDeliveryCostService
    {
        private IProcessDeliveryCostEstimateService _processDeliveryCostEstimateService;
        public DeliveryCostService(IProcessDeliveryCostEstimateService processDeliveryCostEstimateService)
        {
            _processDeliveryCostEstimateService = processDeliveryCostEstimateService;
        }
        /// <summary>
        /// Process Delivery Cost main method
        /// </summary>
        /// <param name="packageDetails"></param>
        /// <returns></returns>
        public List<string> ProcessDeliveryCost(PackageDetails packageDetails)
        {

            List<DeliveryEstimate> deliveryList = [];
            List<string> result = [];

            try
            {
                deliveryList = _processDeliveryCostEstimateService.ProcessDeliveryCostService(packageDetails);

                foreach (var delivery in deliveryList)
                {
                    string deliveryCostWithDiscount = $"{delivery.PackageId} {delivery.Discount} {delivery.TotalCost}";
                    result.Add(deliveryCostWithDiscount);

                }
            }
            catch (Exception ex)
            {
                Console.Write("Error occur while processing indivisual package. Error info :" + ex.Message);

            }

            return result;
        }


    }
}
