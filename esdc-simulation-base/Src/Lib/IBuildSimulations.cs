using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IBuildSimulations<T, U>
        where T : ISimulationCase
        where U : ISimulationCaseRequest
    {
        Simulation<T> Build(SimulationRequest<U> simulationRequest);
    }
}