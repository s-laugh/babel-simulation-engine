
using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Storage
{
    public interface IStoreSimulations<T> where T : ISimulationCase
    {
        void Save(Simulation<T> simulation);
        Simulation<T> Get(Guid id);
        List<Simulation<T>> GetAll();
        void Delete(Guid id);
    }
}