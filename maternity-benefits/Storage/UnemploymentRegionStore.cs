using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace maternity_benefits
{
    public interface IStoreUnemploymentRegions {
        IEnumerable<UnemploymentRegion> GetUnemploymentRegions();
        void AddUnemploymentRegions(IEnumerable<UnemploymentRegion> regions);
        void Clear();
    }

    public class UnemploymentRegionStore : IStoreUnemploymentRegions
    {
        private static readonly string cacheKey = "unemployment_regions";
        private readonly IMemoryCache _cache;

        public UnemploymentRegionStore(IMemoryCache cache) {
            _cache = cache;
        }

        public IEnumerable<UnemploymentRegion> GetUnemploymentRegions() {
            return (List<UnemploymentRegion>)_cache.Get(cacheKey);
        }

        public void AddUnemploymentRegions(IEnumerable<UnemploymentRegion> regions) {
            List<UnemploymentRegion> currList = (List<UnemploymentRegion>)_cache.Get(cacheKey);
            if (currList == null) {
                _cache.Set(cacheKey, regions);
            } else {
                currList.AddRange(regions);
                _cache.Set(cacheKey, currList);
            }
        }

        public void Clear() {
            _cache.Set(cacheKey, new List<UnemploymentRegion>());
        }

    }
}
