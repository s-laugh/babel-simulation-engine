using System;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IHandleSimulationRequests <T> where T : ISimulationCaseRequest
    {
         Guid Handle(SimulationRequest<T> request);
    }
}