using DeliveryEstimationLibrary.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Extensions
{
    public static class OfferCriteriaExtension
    {
        /// <summary>
        /// Validate the offer Coupons
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="distance"></param>
        /// <param name="weight"></param>
        /// <returns> bool value</returns>
        public static bool ValidCouponOffer(this string coupon, int distance, int weight)
        {
            bool validCoupon;
            switch (coupon)
            {
                case CouponConstant.OFR001: validCoupon = distance < 200 && weight >= 70 && weight <= 200 ? true : false; break;
                case CouponConstant.OFR002: validCoupon = distance >= 50 && distance <= 150 && weight >= 100 && weight <= 250 ? true : false; break;
                case CouponConstant.OFR003: validCoupon = distance >= 50 && distance <= 250 && weight >= 10 && weight <= 150 ? true : false; break;
                default: validCoupon = false; break;

            }
            return validCoupon;
        }

        /// <summary>
        /// Calculate the Coupon amount based on the range
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal CalculateCouponOffer(this string coupon, int amount)
        {
            decimal discountAmount;
            switch (coupon)
            {
                case CouponConstant.OFR001: discountAmount = amount * 10 / 100; break;
                case CouponConstant.OFR002: discountAmount = amount * 7 / 100; break;
                case CouponConstant.OFR003: discountAmount = amount * 5 / 100; break;
                default: discountAmount = 0; break;


            }
            return discountAmount;
        }
    }
}
