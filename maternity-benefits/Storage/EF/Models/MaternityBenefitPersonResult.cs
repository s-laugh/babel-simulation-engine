using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace maternity_benefits.Storage.EF.Models
{
    [Table("MaternityBenefitsPersonResult")]
    public class MaternityBenefitsPersonResult
    {
        public Guid Id { get; set; }
        public Guid SimulationResultId { get; set; }
        [ForeignKey("SimulationResultId ")]
        public MaternityBenefitsSimulationResult SimulationResult { get; set; }
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public MaternityBenefitsPerson Person { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal BaseAmount { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal VariantAmount { get; set; }
    }
}