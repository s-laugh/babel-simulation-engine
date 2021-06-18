using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsCase : MaternityBenefitsCaseRequest, ISimulationCase
    {
        public Guid Id { get; set; }

        public MaternityBenefitsCase() : base() {
        }

        public MaternityBenefitsCase(MaternityBenefitsCaseRequest request) {
            Id = Guid.NewGuid();
            NumWeeks = request.NumWeeks;
            MaxWeeklyAmount = request.MaxWeeklyAmount;
            Percentage = request.Percentage;
        }
    }
}
