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
    public class ProcessDeliveryTimeEstimateService : IProcessDeliveryTimeEstimateService
    {
        private IProcessPackageService _processPackageService;
        private IProcessDeliveryCostEstimateService _processDeliveryCostEstimateService;
        public ProcessDeliveryTimeEstimateService(IProcessPackageService processPackageService, IProcessDeliveryCostEstimateService processDeliveryCostEstimateService)
        {
            _processPackageService = processPackageService;
            _processDeliveryCostEstimateService = processDeliveryCostEstimateService;
        }


        /// <summary>
        /// Procss Delivery time service 
        /// </summary>
        /// <param name="packageDetails"></param>
        /// <returns>List of Delivery Estimate </returns>
        public List<DeliveryEstimate> ProcessDeliveryTimeService(PackageDetails packageDetails)
        {
            List<DeliveryEstimate> deliveryList = [];
            var packages = packageDetails.packages;
            int limit = packageDetails.LimitedWeight;

            //PackagePairs gives you pairs package combined weight < 200(given limit size)
            var pairedPackages = packages.PackagePairs(limit);
            List<Dictionary<string, decimal>> listOfPairedVehicleTime = new List<Dictionary<string, decimal>>();
            foreach (var item in pairedPackages)
            {
                var selectedPaired = packages.Where(x => item.Contains(x.PackageId)).ToList();
                var travelTimeList = AllocateVehicleTravelTime(selectedPaired, packageDetails.Speed);
                listOfPairedVehicleTime.Add(travelTimeList);
            }

            //Allocate Number of Vehicle
            var noOfVehicles = Enumerable.Range(1, packageDetails.NoOfVehicles).Select(i => new Vehicle { VehicleId = i }).ToList();
            List<AfterTimeAllocationPackage> AfterTimeAllocationPackagelist = new List<AfterTimeAllocationPackage>();
            foreach (var item in listOfPairedVehicleTime)
            {
                var lowest = noOfVehicles.OrderBy(v => v.TotalTime).First();
                decimal addedTime = 0;
                foreach (var pairs in item)
                {
                    AfterTimeAllocationPackage afterTimeAllocation = new AfterTimeAllocationPackage();
                    afterTimeAllocation.TimeTaken = lowest.TotalTime + pairs.Value;
                    addedTime = Math.Max(addedTime, pairs.Value);
                    afterTimeAllocation.PackageId = pairs.Key;
                    AfterTimeAllocationPackagelist.Add(afterTimeAllocation);
                }
                lowest.TotalTime += (2 * addedTime);
            }

            deliveryList = _processDeliveryCostEstimateService.ProcessDeliveryCostService(packageDetails);
            foreach (var item in deliveryList)
            {
                item.TravelTime = AfterTimeAllocationPackagelist.Where(x => x.PackageId == item.PackageId).Select(x => x.TimeTaken).First();
            }

            return deliveryList;
        }
        /// <summary>
        /// Calculate the package travel time 
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private static Dictionary<string, decimal> AllocateVehicleTravelTime(List<Package> packages, int speed)
        {
            Dictionary<string, decimal> VehicleTravelTime = [];
            foreach (var package in packages)
            {
                VehicleTravelTime[package.PackageId] = package.Distance.CalculateDeliveryTime(speed);
            }
            return VehicleTravelTime;
        }
    }
}
