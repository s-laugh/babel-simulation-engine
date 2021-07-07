using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Rules;
using Rule = esdc_rules_classes.MaternityBenefits;

namespace maternity_benefits
{
    public class MaternityBenefitsRulesRequest : IRulesRequest
    {
        public Rule.MaternityBenefitsCase Rule { get; set; }
        public Rule.MaternityBenefitsPerson Person { get; set; }

    }
}
