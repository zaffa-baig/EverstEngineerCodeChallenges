using DeliveryEstimationLibrary.Constants;
using DeliveryEstimationLibrary.Models;
using DeliveryEstimationLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.UnitTest.Services
{
    public class ProcessPackageServiceTest
    {
        [Fact]
        public void ProcessSinglePackage_Should_Return_Correct_Cost()
        {
            // Arrange
            var service = new ProcessPackageService();
            var package = new Package
            {
                Distance = 20,
                Weight = 5
            };

            // Act
            var result = service.ProcessSinglePackage(package, basePrice: 50);

            // Expected:
            // = basePrice + 10 * weight + 5 * distance
            int expected = 50 +
                           StandardConstant.WeightCost * 5 +
                           StandardConstant.DistanceCost * 20;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProcessSinglePackage_When_Package_Null_Should_Return_Zero()
        {
            // Arrange
            var service = new ProcessPackageService();

            // Act
            var result = service.ProcessSinglePackage(null, 100);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
