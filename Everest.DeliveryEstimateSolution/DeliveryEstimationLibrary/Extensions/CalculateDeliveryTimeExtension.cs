using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Extensions
{

    public static class CalculateDeliveryTimeExtension
    {
        /// <summary>
        /// Calculate the delivery time by distance/speed
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="speed"></param>
        /// <returns> time</returns>
        public static decimal CalculateDeliveryTime(this int distance, int speed)
        {
            var res = (decimal)distance / speed;
            return Math.Truncate(res * 100)/100;
        }
    }
}
