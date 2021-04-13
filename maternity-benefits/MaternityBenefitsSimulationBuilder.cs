using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsSimulationBuilder : IBuildSimulations<MaternityBenefitsCase, MaternityBenefitsCaseRequest>
    {
        private readonly IStoreUnemploymentRegions _regionStore;
        public MaternityBenefitsSimulationBuilder(IStoreUnemploymentRegions regionStore) {
            _regionStore = regionStore;
        }

        public Simulation<MaternityBenefitsCase> Build(SimulationRequest<MaternityBenefitsCaseRequest> simulationRequest) {
            var regions = _regionStore.GetUnemploymentRegions();
            var regionDict = regions.ToDictionary(x => x.Id);

            var baseCase = new MaternityBenefitsCase(simulationRequest.BaseCaseRequest, regionDict);
            var variantCase = new MaternityBenefitsCase(simulationRequest.VariantCaseRequest, regionDict);

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
