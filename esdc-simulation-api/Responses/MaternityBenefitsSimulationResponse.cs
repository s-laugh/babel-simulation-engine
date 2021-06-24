using System;

using esdc_simulation_base.Src.Classes;
using maternity_benefits;

namespace esdc_simulation_api
{
    // TODO: Do some renaming here. Its confusing
    public class MaternityBenefitsSimulationResponse
    {
        public Guid Id { get; set; }
        public string SimulationName { get; set; }
        public DateTime DateCreated { get; set; }
        public MaternityBenefitsCaseRequest BaseCase { get; set; }
        public MaternityBenefitsCaseRequest VariantCase { get; set; }

        public MaternityBenefitsSimulationResponse(Simulation<MaternityBenefitsCase> simulation) 
        {
            Id = simulation.Id;
            SimulationName = simulation.Name;
            DateCreated = simulation.DateCreated;
            BaseCase = (MaternityBenefitsCaseRequest)simulation.BaseCase;
            VariantCase = (MaternityBenefitsCase)simulation.VariantCase;
        }
    }
}