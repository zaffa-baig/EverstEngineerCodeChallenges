using DeliveryEstimationLibrary.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryEstimationLibrary.Extensions;

namespace Everest.UnitTest.Extensions
{
    public class OfferCriteriaExtensionTest
    {
        [Fact]
        public void ValidCouponOffer_OFR001_Should_Return_True()
        {
            bool result = CouponConstant.OFR001.ValidCouponOffer(distance: 150, weight: 100);
            Assert.True(result);
        }

        [Fact]
        public void ValidCouponOffer_OFR001_Should_Return_False_When_DistanceTooHigh()
        {
            bool result = CouponConstant.OFR001.ValidCouponOffer(distance: 250, weight: 100);
            Assert.False(result);
        }

        [Fact]
        public void ValidCouponOffer_OFR002_Should_Return_True()
        {
            bool result = CouponConstant.OFR002.ValidCouponOffer(distance: 100, weight: 150);
            Assert.True(result);
        }

        [Fact]
        public void ValidCouponOffer_OFR002_Should_Return_False_When_WeightTooLow()
        {
            bool result = CouponConstant.OFR002.ValidCouponOffer(distance: 100, weight: 50);
            Assert.False(result);
        }

        [Fact]
        public void ValidCouponOffer_OFR003_Should_Return_True()
        {
            bool result = CouponConstant.OFR003.ValidCouponOffer(distance: 100, weight: 50);
            Assert.True(result);
        }

        [Fact]
        public void ValidCouponOffer_Should_Return_False_For_Invalid_Coupon()
        {
            bool result = "INVALID".ValidCouponOffer(distance: 100, weight: 50);
            Assert.False(result);
        }

        // -------------------------------
        // CalculateCouponOffer Tests
        // -------------------------------

        [Theory]
        [InlineData(CouponConstant.OFR001, 1000, 100)]  // 10%
        [InlineData(CouponConstant.OFR002, 1000, 70)]   // 7%
        [InlineData(CouponConstant.OFR003, 1000, 50)]   // 5%
        public void CalculateCouponOffer_Should_Return_Correct_Discount(string coupon, int amount, decimal expected)
        {
            var discount = coupon.CalculateCouponOffer(amount);
            Assert.Equal(expected, discount);
        }

        [Fact]
        public void CalculateCouponOffer_Should_Return_Zero_For_Invalid_Coupon()
        {
            var discount = "INVALID".CalculateCouponOffer(1000);
            Assert.Equal(0, discount);
        }
    }
}
