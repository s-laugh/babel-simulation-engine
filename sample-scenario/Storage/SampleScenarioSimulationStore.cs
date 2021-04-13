using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace sample_scenario
{
    public class SampleScenarioSimulationStore : IStoreSimulations<SampleScenarioCase>
    {
        private readonly static string cacheKeyBase = "sample_scenario_simulations";
        private readonly IMemoryCache _cache;

        public SampleScenarioSimulationStore(IMemoryCache cache) {
            _cache = cache;
        }

        public void Save(Simulation<SampleScenarioCase> simulation) {
            _cache.Set<Simulation<SampleScenarioCase>>($"{cacheKeyBase}_{simulation.Id}", simulation);
        }
           
        public Simulation<SampleScenarioCase> Get(Guid simulationId) {
           return _cache.Get<Simulation<SampleScenarioCase>>($"{cacheKeyBase}_{simulationId}");
        }

        public void Delete(Guid simulationId) {
            _cache.Remove($"{cacheKeyBase}_{simulationId}");
        }
    }
}
