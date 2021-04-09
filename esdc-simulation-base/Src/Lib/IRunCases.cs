using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IRunCases<T,U> 
        where T : ISimulationCase
        where U : IPerson
    {
        SimulationCaseResult Run(T simulationCase, IEnumerable<U> persons);
    }
}