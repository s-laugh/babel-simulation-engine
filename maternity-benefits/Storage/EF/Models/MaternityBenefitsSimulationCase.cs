using System;

namespace maternity_benefits.Storage.EF.Models
{
    public class MaternityBenefitsSimulationCase
    {
        public Guid Id { get; set; }
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }
    }
}