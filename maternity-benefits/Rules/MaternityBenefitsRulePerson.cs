using System;
using System.Collections.Generic;

namespace maternity_benefits.Rules
{
    public class MaternityBenefitsRulePerson
    {
        public decimal AverageIncome { get; set; }

        public MaternityBenefitsRulePerson(MaternityBenefitsPerson person) {
            AverageIncome = person.AverageIncome;
        }
    }
}
