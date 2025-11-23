using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using Everest.DeliveryTimeEstimation.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.UnitTest
{
    public class DeliveryTimeServiceTest
    {
        [Fact]
        public void ProcessDeliveryTime_ShouldReturnFormattedList()
        {
            // Arrange
            var mockService = new Mock<IProcessDeliveryTimeEstimateService>();

            var packageDetails = new PackageDetails
            {
                BasePrice = 100,
                PackageCount = 2,
                packages = new List<Package>()
            };

            var estimates = new List<DeliveryEstimate>
        {
            new DeliveryEstimate { PackageId = "PKG1", Discount = 10, TotalCost = 90, TravelTime = (decimal)2.5 },
            new DeliveryEstimate { PackageId = "PKG2", Discount = 15, TotalCost = 85, TravelTime = (decimal) 3.0 }
        };

            mockService.Setup(x => x.ProcessDeliveryTimeService(packageDetails))
                       .Returns(estimates);

            var service = new DeliveryTimeService(mockService.Object);

            // Act
            var result = service.ProcessDeliveryTime(packageDetails);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("PKG1 10 90 2.5", result);
            Assert.Contains("PKG2 15 85 3", result);
        }


        [Fact]
        public void ProcessDeliveryTime_WhenExceptionThrown_ShouldReturnEmptyList()
        {
            // Arrange
            var mockService = new Mock<IProcessDeliveryTimeEstimateService>();

            mockService
                .Setup(x => x.ProcessDeliveryTimeService(It.IsAny<PackageDetails>()))
                .Throws(new System.Exception("Test error"));

            var packageDetails = new PackageDetails
            {
                BasePrice = 100,
                PackageCount = 1,
                packages = new List<Package>()
            };

            var service = new DeliveryTimeService(mockService.Object);

            // Act
            var result = service.ProcessDeliveryTime(packageDetails);

            // Assert
            Assert.Empty(result);
        }
    }
}
