// See https://aka.ms/new-console-template for more information
using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using DeliveryEstimationLibrary.Services;
using Everest.DeliveryCostEstimation.Interface;
using Everest.DeliveryCostEstimation.Services;
using System.Runtime.CompilerServices;




Console.WriteLine("Enter the base price and number of packages:");
string firstLine = Console.ReadLine();

IProcessPackageService _processPackageService =  new ProcessPackageService();
IProcessDeliveryCostEstimateService _processDeliveryCostEstimateService  = new ProcessDeliveryCostEstimateService(_processPackageService);
IDeliveryCostService _deliveryCostService = new DeliveryCostService(_processDeliveryCostEstimateService);
List<string> packageList = new List<string>();

if (firstLine != null)
{
    var firstParts = firstLine.Split(' ');

    PackageDetails packageDetails = new PackageDetails();
    packageDetails.BasePrice = int.Parse(firstParts[0]);
    packageDetails.PackageCount = int.Parse(firstParts[1]);

    for (int i = 0; i < packageDetails.PackageCount; i++)
    {
        Package package = new Package();
      var packegeInfo =  Console.ReadLine();
        if (packegeInfo != null)
        {
            var packageParts = packegeInfo.Split(' ');
            package.PackageId = packageParts[0];
            package.Weight = int.Parse(packageParts[1]);
            package.Distance = int.Parse(packageParts[2]);
            package.OfferCode = packageParts[3];
        }
        packageDetails.packages.Add(package);
    }
    if( packageDetails != null && packageDetails.packages.Count > 0)
    {
        packageList =  _deliveryCostService.ProcessDeliveryCost(packageDetails);
    }

    if (packageList.Count > 0)
    {
        foreach (var item in packageList)
        {
            Console.WriteLine(item);
        }
    }
}