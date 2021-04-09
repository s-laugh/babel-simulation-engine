using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IRunSimulations<T, U>
        where T : ISimulationCase
        where U : IPerson
    {
         SimulationResult Run(Simulation<T> simulation, IEnumerable<U> persons);
    }
}