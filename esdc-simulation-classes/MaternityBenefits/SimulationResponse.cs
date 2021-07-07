using System;


namespace esdc_simulation_classes.MaternityBenefits
{
    public class SimulationResponse
    {
        public Guid Id { get; set; }
        public string SimulationName { get; set; }
        public DateTime DateCreated { get; set; }
        public CaseRequest BaseCase { get; set; }
        public CaseRequest VariantCase { get; set; }

    }
}