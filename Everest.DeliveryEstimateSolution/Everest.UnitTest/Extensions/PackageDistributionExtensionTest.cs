using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryEstimationLibrary.Extensions;

namespace Everest.UnitTest.Extensions
{
    public class PackageDistributionExtensionTest
    {

        private Package P(string id, int weight) =>
        new Package { PackageId = id, Weight = weight };

        [Fact]
        public void PackagePairs_Should_Return_Best_Pair_And_Singles()
        {
            var packages = new List<Package>
        {
            P("P1", 50),
            P("P2", 40),
            P("P3", 60),
            P("P4", 20)
        };
            int limit = 100;

            // Best pair < 100 is P3 (60) + P2 (40) = 100? No (< limit) → pair = 60+40=100 NO (CEILING)
            // Next best < 100 = P1(50) + P3(60)=110 NO
            // P1(50) + P2(40)=90 (VALID & best)

            var result = packages.PackagePairs(limit);

            Assert.Equal(3, result.Count);

            Assert.Equal(new List<string> { "P1", "P2" }, result[0]);

            // remaining sorted by weight desc
            Assert.Equal(new List<string> { "P3" }, result[1]);
            Assert.Equal(new List<string> { "P4" }, result[2]);
        }

        [Fact]
        public void PackagePairs_When_No_Pairs_Possible_Should_All_Be_Singles()
        {
            var packages = new List<Package>
        {
            P("P1", 90),
            P("P2", 95),
            P("P3", 93)
        };
            int limit = 100;

            var result = packages.PackagePairs(limit);

            Assert.Equal(3, result.Count);
            Assert.Equal("P2", result[0].Single());
            Assert.Equal("P3", result[1].Single());
            Assert.Equal("P1", result[2].Single());
        }

        [Fact]
        public void PackagePairs_With_Exact_TwoPackages_Should_Return_Pair_If_Valid()
        {
            var packages = new List<Package>
        {
            P("P1", 40),
            P("P2", 50)
        };

            var result = packages.PackagePairs(100);

            Assert.Single(result);
            Assert.Equal(new List<string> { "P1", "P2" }, result[0]);
        }

        [Fact]
        public void PackagePairs_TwoPackages_Should_Return_Singles_If_Invalid()
        {
            var packages = new List<Package>
        {
            P("P1", 80),
            P("P2", 90)
        };

            var result = packages.PackagePairs(100);

            Assert.Equal(2, result.Count);
            Assert.Equal("P2", result[0].Single());
            Assert.Equal("P1", result[1].Single());
        }
    }
}
