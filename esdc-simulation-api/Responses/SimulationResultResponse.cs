using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_api
{
    public class SimulationResultResponse
    {
        public MaternityBenefitsSimulationResponse Simulation { get; set; }
        // May just have my own PersonResponse...?
        public MaternityBenefitsSimulationResult Result { get; set; }
    }
}