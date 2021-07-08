using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Rules;
using Rule = esdc_rules_classes.MaternityBenefits;

namespace maternity_benefits
{
    public class MaternityBenefitsBulkExecutor : IExecuteBulkRules<MaternityBenefitsCase, MaternityBenefitsPerson>
    {
        private readonly IRulesEngine _rulesApi;
        private static readonly string ENDPOINT = "MaternityBenefits/Bulk";
        public MaternityBenefitsBulkExecutor(IRulesEngine rulesApi) {
            _rulesApi = rulesApi;
        }

        public Dictionary<Guid, decimal> Execute(MaternityBenefitsCase simulationCase, IEnumerable<MaternityBenefitsPerson> persons) {
            var rule = new Rule.MaternityBenefitsCase() {
                NumWeeks = simulationCase.NumWeeks,
                MaxWeeklyAmount = simulationCase.MaxWeeklyAmount,
                Percentage = simulationCase.Percentage
            };
            var rulePersons = persons.Select(x => new Rule.MaternityBenefitsPerson() {
                Id = x.Id,
                AverageIncome = x.AverageIncome
            }).ToList();
            var rulesReq = new Rule.MaternityBenefitsBulkRequest() {
                Rule = rule,
                Persons = rulePersons
            };
            var result =  _rulesApi.Execute<Rule.MaternityBenefitsBulkResponse>(ENDPOINT, rulesReq);
            return result.ResponseDict.ToDictionary(x => x.Key, y => y.Value.Amount);
        }
    }
}
