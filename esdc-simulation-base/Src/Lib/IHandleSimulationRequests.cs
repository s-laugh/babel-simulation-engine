using System;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IHandleSimulationRequests<T> where T: ISimulationCase
    {
         Guid Handle(Simulation<T> request);
    }
}