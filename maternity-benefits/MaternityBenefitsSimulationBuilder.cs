using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsSimulationBuilder : IBuildSimulations<MaternityBenefitsCase, MaternityBenefitsCaseRequest>
    {
        public Simulation<MaternityBenefitsCase> Build(SimulationRequest<MaternityBenefitsCaseRequest> simulationRequest) {
            var baseCase = new MaternityBenefitsCase(simulationRequest.BaseCaseRequest);
            var variantCase = new MaternityBenefitsCase(simulationRequest.VariantCaseRequest);

            return new Simulation<MaternityBenefitsCase>() {
                Id = Guid.NewGuid(),
                Name = simulationRequest.SimulationName,
                DateCreated = DateTime.Now,
                BaseCase = baseCase,
                VariantCase = variantCase,
            };
        }

    }
}
