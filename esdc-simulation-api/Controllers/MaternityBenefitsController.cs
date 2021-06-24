using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using maternity_benefits;
using maternity_benefits.Storage.Mock;

namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleSimulationRequests<MaternityBenefitsCaseRequest> _requestHandler;
        private readonly IStoreSimulations<MaternityBenefitsCase> _simulationStore;
        private readonly IStoreSimulationResults<MaternityBenefitsCase> _resultStore;

        public MaternityBenefitsController(
            IHandleSimulationRequests<MaternityBenefitsCaseRequest> requestHandler,
            IStoreSimulations<MaternityBenefitsCase> simulationStore,
            IStoreSimulationResults<MaternityBenefitsCase> resultStore
        )
        {
            _requestHandler = requestHandler;
            _simulationStore = simulationStore;
            _resultStore = resultStore;
        }

        [HttpGet("{simulationId}")]
        public MaternityBenefitsSimulationResponse GetSimulation(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            return new MaternityBenefitsSimulationResponse(simulation);
        }

        [HttpGet]
        public AllSimulationsResponse GetAllSimulations()
        {
            var result = new List<MaternityBenefitsSimulationResponse>();
            var simulations = _simulationStore.GetAll();
            foreach (var sim in simulations) {
                result.Add(new MaternityBenefitsSimulationResponse(sim));
            }
            return new AllSimulationsResponse() {
                Simulations = result
            };
        }

        [HttpPost]
        public CreateSimulationResponse CreateSimulation(SimulationRequest<MaternityBenefitsCaseRequest> request)
        {
            var simulationId = _requestHandler.Handle(request);
            return new CreateSimulationResponse {
                Id = simulationId
            };
        }

        // [HttpGet("{simulationId}/Results")]
        // public SimulationResult GetResult(Guid simulationId)
        // {
        //     var result = _resultStore.Get(simulationId);
        //     return result;
        // }

        [HttpGet("{simulationId}/Results")]
        public SimulationResultResponse GetFullResult(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            var result = _resultStore.Get(simulationId);

            return new SimulationResultResponse() {
                Simulation = new MaternityBenefitsSimulationResponse(simulation),
                Result = new MaternityBenefitsSimulationResult(result)

            };
        }

        // TODO: Need a delete

    }
}
