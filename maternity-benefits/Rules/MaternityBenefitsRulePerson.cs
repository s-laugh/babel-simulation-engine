using System;
using System.Collections.Generic;

namespace maternity_benefits.Rules
{
    public class MaternityBenefitsRulePerson
    {
        public Guid UnemploymentRegionId { get; set; }
        public List<WeeklyIncome> WeeklyIncome { get; set; }

        public MaternityBenefitsRulePerson(MaternityBenefitsPerson person) {
            UnemploymentRegionId = person.UnemploymentRegion.Id;
            WeeklyIncome = new List<WeeklyIncome>(person.WeeklyIncome);
        }
    }
}
