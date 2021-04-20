using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Rules;
using maternity_benefits.Rules;

namespace maternity_benefits
{
    public class MaternityBenefitsExecutor : IExecuteRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        private readonly IRulesEngine<MaternityBenefitsRulesRequest> _rulesApi;
        private static readonly string ENDPOINT = "MaternityBenefits";
        public MaternityBenefitsExecutor(IRulesEngine<MaternityBenefitsRulesRequest> rulesApi) {
            _rulesApi = rulesApi;
        }

        public decimal Execute(MaternityBenefitsCase simulationCase, MaternityBenefitsPerson person) {
            var rulesReq = new MaternityBenefitsRulesRequest() {
                Rule = new MaternityBenefitsRule(simulationCase),
                Person = new MaternityBenefitsRulePerson(person)
            };
            return _rulesApi.Execute<decimal>(ENDPOINT, rulesReq);
        }
    }
}
