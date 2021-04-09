
using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Storage
{
    public interface IStoreSimulations<T> where T : ISimulationCase
    {
        void SaveSimulation(Simulation<T> simulation);
        void StoreResults(Guid simulationId, SimulationResult simulationResult);
        Simulation<T> GetSimulation(Guid id);
        SimulationResult GetSimulationResult(Guid simulationId);
        void Delete(Guid id);
    }
}