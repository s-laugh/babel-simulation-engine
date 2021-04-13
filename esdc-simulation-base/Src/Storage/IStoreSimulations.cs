
using System;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Storage
{
    public interface IStoreSimulations<T> where T : ISimulationCase
    {
        void Save(Simulation<T> simulation);
        Simulation<T> Get(Guid id);
        void Delete(Guid id);
    }
}