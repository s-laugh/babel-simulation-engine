
using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Storage
{
    public interface IStoreSimulationResults<T> where T : ISimulationCase
    {
        void Save(Guid simulationId, SimulationResult simulationResult);
        SimulationResult Get(Guid simulationId);
        void Delete(Guid id);
    }
}