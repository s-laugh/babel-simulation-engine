using System;

using esdc_simulation_base.Src.Classes;

namespace sample_scenario
{
    public class SampleScenarioCase : SampleScenarioCaseRequest, ISimulationCase
    {
        public Guid Id { get; set; }
    }
}
