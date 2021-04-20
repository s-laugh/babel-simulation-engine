using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Rules;

namespace maternity_benefits.Rules
{
    public class MaternityBenefitsRulesRequest : IRulesRequest
    {
        public MaternityBenefitsRule Rule { get; set; }
        public MaternityBenefitsRulePerson Person { get; set; }

    }
}
