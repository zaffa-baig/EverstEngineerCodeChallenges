using DeliveryEstimationLibrary.Interface;
using DeliveryEstimationLibrary.Models;
using Everest.DeliveryCostEstimation.Services;
using Moq;
using Xunit;

namespace Everest.UnitTest
{
    public class DeliveryCostServiceTest
    {
        [Fact]
        public void ProcessDeliveryCost_ShouldReturnFormattedList()
        {
            // Arrange
            var mockService = new Mock<IProcessDeliveryCostEstimateService>();

            var packageDetails = new PackageDetails
            {
                BasePrice = 100,
                PackageCount = 2,
                packages = new List<Package>()
            };

            var estimates = new List<DeliveryEstimate>
        {
            new DeliveryEstimate { PackageId = "PKG1", Discount = 10, TotalCost = 90 },
            new DeliveryEstimate { PackageId = "PKG2", Discount = 5, TotalCost = 95 }
        };

            mockService
                .Setup(x => x.ProcessDeliveryCostService(packageDetails))
                .Returns(estimates);

            var service = new DeliveryCostService(mockService.Object);

            // Act
            var result = service.ProcessDeliveryCost(packageDetails);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("PKG1 10 90", result);
            Assert.Contains("PKG2 5 95", result);
        }

        [Fact]
        public void ProcessDeliveryCost_WhenExceptionThrown_ShouldReturnEmptyList()
        {
            // Arrange
            var mockService = new Mock<IProcessDeliveryCostEstimateService>();

            mockService
                .Setup(x => x.ProcessDeliveryCostService(It.IsAny<PackageDetails>()))
                .Throws(new System.Exception("Test Error"));

            var service = new DeliveryCostService(mockService.Object);

            var packageDetails = new PackageDetails
            {
                BasePrice = 100,
                PackageCount = 1,
                packages = new List<Package>()
            };

            // Act
            var result = service.ProcessDeliveryCost(packageDetails);

            // Assert
            Assert.Empty(result);
        }
    }
}
