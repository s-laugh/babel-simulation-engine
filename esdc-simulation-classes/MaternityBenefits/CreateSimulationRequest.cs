using System;

namespace esdc_simulation_classes.MaternityBenefits
{
    public class CreateSimulationRequest
    {
        public string SimulationName { get; set; }
        public CaseRequest BaseCaseRequest { get; set; }
        public CaseRequest VariantCaseRequest { get; set; }
    }
}