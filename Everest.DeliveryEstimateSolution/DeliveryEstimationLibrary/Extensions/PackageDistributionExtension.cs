using DeliveryEstimationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryEstimationLibrary.Extensions
{
    public static class PackageDistributionExtension
    {
        /// <summary>
        /// Logic to pair the packages based on the limit weight 
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="limit"></param>
        /// <returns>List</returns>
        public static List<List<string>> PackagePairs(this List<Package> packages, int limit)
        {
            List<List<string>> result = new();

            var list = packages.ToList();
            int n = list.Count;

            // Step 1: Find best pair (max sum < limit)
            int bestI = -1, bestJ = -1, bestSum = -1;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int sum = list[i].Weight + list[j].Weight;
                    if (sum < limit && sum > bestSum)
                    {
                        bestSum = sum;
                        bestI = i;
                        bestJ = j;
                    }
                }
            }
            var dictionary = packages.ToDictionary(p => p.PackageId, p => p.Weight);
            var remaining = new List<KeyValuePair<string, int>>(dictionary);

            // Step 2: Add the best pair first
            if (bestI != -1)
            {
                var pair = new List<string> { list[bestI].PackageId, list[bestJ].PackageId };
                result.Add(pair);

                // remove paired items
                remaining.RemoveAll(x => x.Key == list[bestI].PackageId || x.Key == list[bestJ].PackageId);
            }

            // Step 3: remaining (singles) -> sort heavy first
            foreach (var item in remaining.OrderByDescending(x => x.Value))
            {
                result.Add(new List<string> { item.Key });
            }

            return result;
        }
    }
}
