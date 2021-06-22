using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace maternity_benefits.Storage.Cache
{
    public class MaternityBenefitsSimulationStore : IStoreSimulations<MaternityBenefitsCase>
    {
        private readonly static string cacheKeyBase = "maternity_benefits_simulations";
        private readonly IMemoryCache _cache;

        public MaternityBenefitsSimulationStore(IMemoryCache cache) {
            _cache = cache;
        }

        public void Save(Simulation<MaternityBenefitsCase> simulation) {
            _cache.Set<Simulation<MaternityBenefitsCase>>($"{cacheKeyBase}_{simulation.Id}", simulation);
            
        }

        public Simulation<MaternityBenefitsCase> Get(Guid simulationId) {
            return _cache.Get<Simulation<MaternityBenefitsCase>>($"{cacheKeyBase}_{simulationId}");
        }

        public void Delete(Guid simulationId) {
           _cache.Remove($"{cacheKeyBase}_{simulationId}");
        }
    }
}
