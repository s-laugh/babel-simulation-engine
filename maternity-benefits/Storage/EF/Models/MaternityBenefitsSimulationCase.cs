using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace maternity_benefits.Storage.EF.Models
{
    [Table("MaternityBenefitsSimulationCase")]
    public class MaternityBenefitsSimulationCase
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }
    }
}