using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Rules;

using sample_scenario;
using sample_scenario.Rules;

namespace sample_scenario
{
    public class SampleScenarioExecutor : IExecuteRules<SampleScenarioCase, SampleScenarioPerson>
    {
        private readonly IRulesEngine<SampleScenarioRulesRequest> _rulesApi;
        private static readonly string ENDPOINT = "SampleScenario";

        public SampleScenarioExecutor(IRulesEngine<SampleScenarioRulesRequest> rulesApi) {
            _rulesApi = rulesApi;
        }
        public decimal Execute(SampleScenarioCase simulationCase, SampleScenarioPerson person) {
            var rulesReq = new SampleScenarioRulesRequest(simulationCase, person);
            return _rulesApi.Execute<decimal>(ENDPOINT, rulesReq);
        }
    }
}
