using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;

namespace maternity_benefits
{
    public class MaternityBenefitsExecutor : IExecuteRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        public decimal Execute(MaternityBenefitsCase simulationCase, MaternityBenefitsPerson person) {
            var bestWeeksDict = simulationCase.RegionDict.ToDictionary(x => x.Key, x => x.Value.BestWeeks);
            
            int bestWeeks = bestWeeksDict[person.UnemploymentRegion.Id];
            decimal averageIncome = person.GetAverageIncome(bestWeeks); 

            decimal temp = averageIncome * (decimal)simulationCase.Percentage/100;
            temp = Math.Min(temp, simulationCase.MaxWeeklyAmount);
            decimal amount = temp * simulationCase.NumWeeks;

            return amount;
        }
    }
}
