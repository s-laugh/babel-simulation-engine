using System;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace sample_scenario
{
    public class SampleScenarioSimulationBuilder : IBuildSimulations<SampleScenarioCase, SampleScenarioCaseRequest>
    {
        public Simulation<SampleScenarioCase> Build(SimulationRequest<SampleScenarioCaseRequest> simulationRequest) {
            var baseCase = new SampleScenarioCase(simulationRequest.BaseCaseRequest);
            var variantCase = new SampleScenarioCase(simulationRequest.VariantCaseRequest);

            return new Simulation<SampleScenarioCase>() {
                Id = Guid.NewGuid(),
                Name = simulationRequest.SimulationName,
                DateCreated = DateTime.Now,
                BaseCase = baseCase,
                VariantCase = variantCase,
            };
        }
    }
}
