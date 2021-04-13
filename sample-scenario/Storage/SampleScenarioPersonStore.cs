using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using esdc_simulation_base.Src.Storage;

namespace sample_scenario
{
    public class SampleScenarioPersonStore : IStorePersons<SampleScenarioPerson>
    {
        private readonly static string cacheKeyBase = "sample_scenario";
        private readonly static string cacheKeyPersons = $"{cacheKeyBase}_persons";
        private readonly IMemoryCache _cache;

        public SampleScenarioPersonStore(IMemoryCache cache) {
            _cache = cache;
        }

        public IEnumerable<SampleScenarioPerson> GetAllPersons() {
            return (List<SampleScenarioPerson>)_cache.Get(cacheKeyPersons);
        }

        public void AddPersons(IEnumerable<SampleScenarioPerson> persons) {
            List<SampleScenarioPerson> currList = (List<SampleScenarioPerson>)_cache.Get(cacheKeyPersons);
            currList.AddRange(persons);
            _cache.Set(cacheKeyPersons, currList);
        }

        public void Clear() {
            _cache.Set(cacheKeyPersons, new List<SampleScenarioPerson>());
        }
    }
}
