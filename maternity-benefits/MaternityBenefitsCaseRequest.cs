using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsCaseRequest : ISimulationCaseRequest
    {
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }

        public MaternityBenefitsCaseRequest() {
            Percentage = 0;
            MaxWeeklyAmount = 0;
            NumWeeks = 0;
        }
    }
}
