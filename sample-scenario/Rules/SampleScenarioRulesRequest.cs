using System;

using esdc_simulation_base.Src.Rules;


namespace sample_scenario.Rules
{
    public class SampleScenarioRulesRequest : IRulesRequest
    {
        public SampleScenarioCase Rule { get; set; }
        public SampleScenarioPerson Person { get; set; }

        public SampleScenarioRulesRequest(SampleScenarioCase simulationCase, SampleScenarioPerson person) {
            Rule = simulationCase;
            Person = person;
        }

    }
}
