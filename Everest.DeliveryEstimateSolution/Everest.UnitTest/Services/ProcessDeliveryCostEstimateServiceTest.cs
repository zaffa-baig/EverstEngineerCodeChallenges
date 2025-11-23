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
    public class ProcessDeliveryCostEstimateServiceTest
    {
        [Fact]
        public void ProcessDeliveryCostService_Should_Return_Correct_Cost_Without_Discount()
        {
            // Arrange
            var mockProcessPackage = new Mock<IProcessPackageService>();
            mockProcessPackage.Setup(x => x.ProcessSinglePackage(It.IsAny<Package>(), 100))
                              .Returns(200);

            var package = new Package
            {
                PackageId = "PKG1",
                Weight = 5,
                Distance = 10,
                OfferCode = "OFR003"
            };

            var packageDetails = new PackageDetails
            {
                BasePrice = 100,
                packages = new List<Package> { package }
            };

            var service = new ProcessDeliveryCostEstimateService(mockProcessPackage.Object);

            // Act
            var result = service.ProcessDeliveryCostService(packageDetails);

            // Assert
            Assert.Single(result);
            Assert.Equal("PKG1", result[0].PackageId);
            Assert.Equal(200, result[0].TotalCost);   // no discount
            Assert.Equal(0, result[0].Discount);
        }

        [Fact]
        public void ProcessDeliveryCostService_Should_Apply_Discount_When_Valid()
        {
            // Arrange
            var mockProcessPackage = new Mock<IProcessPackageService>();
            mockProcessPackage.Setup(x => x.ProcessSinglePackage(It.IsAny<Package>(), 150))
                              .Returns(300);

            var package = new Package
            {
                PackageId = "PKG2",
                Weight = 10,
                Distance = 20,
                OfferCode = "OFR003"
            };

            var details = new PackageDetails
            {
                BasePrice = 150,
                packages = new List<Package> { package }
            };

            var service = new ProcessDeliveryCostEstimateService(mockProcessPackage.Object);

            // Act
            var result = service.ProcessDeliveryCostService(details);

            // Assert
            Assert.Single(result);
            Assert.Equal("PKG2", result[0].PackageId);
            Assert.Equal(300, result[0].TotalCost); // 300 - 50
            Assert.Equal(0, result[0].Discount);
        }

        [Fact]
        public void ProcessDeliveryCostService_Should_Call_ProcessSinglePackage_For_Each_Package()
        {
            // Arrange
            var mockProcessPackage = new Mock<IProcessPackageService>();
            mockProcessPackage.Setup(x => x.ProcessSinglePackage(It.IsAny<Package>(), It.IsAny<int>()))
                              .Returns(100);

            var packages = new List<Package>
        {
            new Package { PackageId = "P1", OfferCode = "OFR003" },
            new Package { PackageId = "P2", OfferCode = "OFR003" }
        };

            var details = new PackageDetails
            {
                BasePrice = 100,
                packages = packages
            };

            var service = new ProcessDeliveryCostEstimateService(mockProcessPackage.Object);

            // Act
            service.ProcessDeliveryCostService(details);

            // Assert
            mockProcessPackage.Verify(x =>
                x.ProcessSinglePackage(It.IsAny<Package>(), 100), Times.Exactly(2));
        }

       
    }
}
