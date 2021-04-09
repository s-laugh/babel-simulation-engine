using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace sample_scenario
{
    public class SampleScenarioCaseRequest : ISimulationCaseRequest
    {
        public bool SomeToggle { get; set; }
        public int SomeFactor { get; set; }
        public double SomeThreshold { get; set; }
    }
}
