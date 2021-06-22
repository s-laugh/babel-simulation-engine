using System;

namespace maternity_benefits.Storage.EF.Models
{
    public class PersonResult
    {
        public Guid Id { get; set; }
        public Guid MaternityBenefitsSimulationResultId { get; set; }
        public MaternityBenefitsSimulationResult MaternityBenefitsSimulationResult { get; set; }
        public Guid MaternityBenefitsPersonId { get; set; }
        public MaternityBenefitsPerson MaternityBenefitsPerson { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal VariantAmount { get; set; }
    }
}