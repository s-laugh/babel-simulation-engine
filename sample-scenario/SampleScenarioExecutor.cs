using System;

using esdc_simulation_base.Src.Lib;

namespace sample_scenario
{
    public class SampleScenarioExecutor : IExecuteRules<SampleScenarioCase, SampleScenarioPerson>
    {
        public decimal Execute(SampleScenarioCase simulationCase, SampleScenarioPerson person) {
            // This is where we call the rules
            return 0m;
        }
    }
}
