
namespace esdc_simulation_base.Src.Classes
{
    public class SimulationRequest<T> where T : ISimulationCaseRequest
    {
        public string SimulationName { get; set; }
        public T BaseCaseRequest { get; set; }
        public T VariantCaseRequest { get; set; }

    }
}