using System;
using System.Collections.Generic;

namespace esdc_simulation_api
{
    public class AllSimulationsResponse
    {
        public List<MaternityBenefitsSimulationResponse> Simulations { get; set; }
    }
}