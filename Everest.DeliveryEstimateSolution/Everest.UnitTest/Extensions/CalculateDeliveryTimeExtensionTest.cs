using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryEstimationLibrary.Extensions;

namespace Everest.UnitTest.Extensions
{
    public class CalculateDeliveryTimeExtensionTest
    {
        [Theory]
        [InlineData(100, 50, 2.00)]
        [InlineData(95, 10, 9.50)]
        [InlineData(23, 7, 3.28)]   // 23/7 = 3.2857 → 3.28
        [InlineData(5, 2, 2.50)]
        public void CalculateDeliveryTime_Should_Truncate_To_Two_Decimals(int distance, int speed, decimal expected)
        {
            // Act
            var result = distance.CalculateDeliveryTime(speed);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
