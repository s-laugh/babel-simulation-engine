using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace maternity_benefits.Storage.EF.Models
{
    [Table("MaternityBenefitsSimulation")]
    public class MaternityBenefitsSimulation
    {
        public Guid Id { get; set; }
        public Guid BaseCaseId { get; set; }
        [ForeignKey("BaseCaseId ")]
        public MaternityBenefitsSimulationCase BaseCase { get; set; }
        public Guid VariantCaseId { get; set; }
        [ForeignKey("VariantCaseId ")]
        public MaternityBenefitsSimulationCase VariantCase { get; set; }
        public string SimulationName { get; set; }
        public DateTime DateCreated { get; set; }

    }
}