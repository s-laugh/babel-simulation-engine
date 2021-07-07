using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Rules;
using Rule = esdc_rules_classes.MaternityBenefits;

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
            var rule = new Rule.MaternityBenefitsCase() {
                NumWeeks = simulationCase.NumWeeks,
                MaxWeeklyAmount = simulationCase.MaxWeeklyAmount,
                Percentage = simulationCase.Percentage
            };
            var rulePerson = new Rule.MaternityBenefitsPerson() {
                AverageIncome = person.AverageIncome
            };
            var rulesReq = new MaternityBenefitsRulesRequest() {
                Rule = rule,
                Person = rulePerson
            };
            
            return _rulesApi.Execute<decimal>(ENDPOINT, rulesReq);
        }
    }
}
