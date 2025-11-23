using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using DeliveryEstimationLibrary.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.UnitTest.Services
{
    public class ProcessDeliveryTimeEstimateServiceTest
    {
        [Fact]
        public void ProcessDeliveryTimeService_Should_Return_Cost_And_Time()
        {
            // Arrange
            var mockPackageService = new Mock<IProcessPackageService>();
            mockPackageService
                .Setup(x => x.ProcessSinglePackage(It.IsAny<Package>(), It.IsAny<int>()))
                .Returns(200);

            var mockCostService = new Mock<IProcessDeliveryCostEstimateService>();
            mockCostService
                .Setup(x => x.ProcessDeliveryCostService(It.IsAny<PackageDetails>()))
                .Returns(new List<DeliveryEstimate>
                {
                new DeliveryEstimate { PackageId = "PKG1", TotalCost = 200 },
                new DeliveryEstimate { PackageId = "PKG2", TotalCost = 250 }
                });

            var packages = new List<Package>
        {
            new Package { PackageId="PKG1", Distance=30, Weight=5, OfferCode = "OFR003" },
            new Package { PackageId="PKG2", Distance=40, Weight=10, OfferCode = "OFR003" }
        };

            var details = new PackageDetails
            {
                BasePrice = 100,
                NoOfVehicles = 2,
                Speed = 10,
                LimitedWeight = 50,
                packages = packages
            };

            var service = new ProcessDeliveryTimeEstimateService(
                mockPackageService.Object,
                mockCostService.Object);

            // Act
            var result = service.ProcessDeliveryTimeService(details);

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Contains(result, r => r.PackageId == "PKG1");
            Assert.Contains(result, r => r.PackageId == "PKG2");

            Assert.True(result.First().TravelTime > 0);
        }

        [Fact]
        public void ProcessDeliveryTimeService_Should_Call_Cost_Service_Once()
        {
            // Arrange
            var mockPackageService = new Mock<IProcessPackageService>();
            mockPackageService
                .Setup(x => x.ProcessSinglePackage(It.IsAny<Package>(), It.IsAny<int>()))
                .Returns(200);

            var mockCostService = new Mock<IProcessDeliveryCostEstimateService>();
            mockCostService
                .Setup(x => x.ProcessDeliveryCostService(It.IsAny<PackageDetails>()))
                .Returns(new List<DeliveryEstimate>());

            var details = new PackageDetails
            {
                BasePrice = 100,
                NoOfVehicles = 2,
                LimitedWeight = 50,
                Speed = 10,
                packages = new List<Package>
            {
                new Package { PackageId="PKG1", Distance=30, OfferCode="OFR003" }
            }
            };

            var service = new ProcessDeliveryTimeEstimateService(
                mockPackageService.Object,
                mockCostService.Object);

            // Act
            service.ProcessDeliveryTimeService(details);

            // Assert
            mockCostService.Verify(x =>
                x.ProcessDeliveryCostService(It.IsAny<PackageDetails>()), Times.Once);
        }
    }
}
