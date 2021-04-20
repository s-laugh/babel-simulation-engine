using System;
using System.Collections.Generic;
using System.Linq;

namespace maternity_benefits.Rules
{
    public class MaternityBenefitsRule
    {
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }
        public Dictionary<Guid, int> BestWeeksDict { get; set; }

        public MaternityBenefitsRule(MaternityBenefitsCase simulationCase) {
            MaxWeeklyAmount = simulationCase.MaxWeeklyAmount;
            Percentage = simulationCase.Percentage;
            NumWeeks = simulationCase.NumWeeks;
            BestWeeksDict = simulationCase.RegionDict.ToDictionary(x => x.Key, x => x.Value.BestWeeks);
        }

    }
}
