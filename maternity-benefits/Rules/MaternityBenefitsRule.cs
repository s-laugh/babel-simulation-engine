using System;

namespace maternity_benefits.Rules
{
    public class MaternityBenefitsRule
    {
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }

        public MaternityBenefitsRule(MaternityBenefitsCase simulationCase) {
            MaxWeeklyAmount = simulationCase.MaxWeeklyAmount;
            Percentage = simulationCase.Percentage;
            NumWeeks = simulationCase.NumWeeks;
        }

    }
}
