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
        //private readonly IMemoryCache _cache;

        public SampleScenarioSimulationStore() {
            //_cache = cache;
        }

        public void SaveSimulation(Simulation<SampleScenarioCase> simulation) {
            //_cache.Set<Simulation<SampleScenarioCase>>("sample_sim", simulation);
            
        }

        public void StoreResults(Guid simulationId, SimulationResult simulationResult) {
           // _cache.Set<SimulationResult>($"sample_sim_result_{simulationId}", simulationResult);
        } 
           
        public Simulation<SampleScenarioCase> GetSimulation(Guid id) {
            return new Simulation<SampleScenarioCase>();
           // return _cache.Get<Simulation<SampleScenarioCase>>("sample_sim");
        }

        public SimulationResult GetSimulationResult(Guid simulationId) {
            return new SimulationResult();
           // return _cache.Get<SimulationResult>($"sample_sim_result_{simulationId}");
        }

        public void Delete(Guid id) {
           // _cache.Remove($"sample_sim");
           // _cache.Remove($"sample_sim_result_{id}");
        }
    }
}
