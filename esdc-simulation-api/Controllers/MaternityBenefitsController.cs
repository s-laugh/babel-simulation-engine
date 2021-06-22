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
        private readonly IHandlePersonCreationRequests<MaternityBenefitsPersonRequest> _personRequestHandler;
        private readonly IStorePersons<MaternityBenefitsPerson> _personStore;
        private readonly IStoreSimulations<MaternityBenefitsCase> _simulationStore;
        private readonly IStoreSimulationResults<MaternityBenefitsCase> _resultStore;

        public MaternityBenefitsController(
            IHandleSimulationRequests<MaternityBenefitsCaseRequest> requestHandler,
            IHandlePersonCreationRequests<MaternityBenefitsPersonRequest> personRequestHandler,
            IStoreSimulations<MaternityBenefitsCase> simulationStore,
            IStorePersons<MaternityBenefitsPerson> personStore,
            IStoreSimulationResults<MaternityBenefitsCase> resultStore
        )
        {
            _requestHandler = requestHandler;
            _personRequestHandler = personRequestHandler;
            _simulationStore = simulationStore;
            _personStore = personStore;
            _resultStore = resultStore;
        }

        [HttpGet("{simulationId}")]
        public MaternityBenefitsSimulationResponse GetSimulation(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            return new MaternityBenefitsSimulationResponse(simulation);
        }

        [HttpPost]
        public CreateSimulationResponse CreateSimulation(SimulationRequest<MaternityBenefitsCaseRequest> request)
        {
            var simulationId = _requestHandler.Handle(request);
            return new CreateSimulationResponse {
                Id = simulationId
            };
        }

        [HttpGet("Persons")]
        public IEnumerable<MaternityBenefitsPerson> GetPersons()
        {
            // TODO: This may end up being too big a response. Maybe a summary?
            var persons = _personStore.GetAllPersons();
            return persons;
        }

        
        [HttpPost("Persons")]
        public void AddPersons(List<MaternityBenefitsPersonRequest> personsRequest)
        {
            _personRequestHandler.Handle(personsRequest);
            return;
        }

        [HttpDelete("Persons")]
        public void DeletePersons()
        {
            _personStore.Clear();
        }

        [HttpGet("{simulationId}/Results")]
        public SimulationResult GetResult(Guid simulationId)
        {
            var result = _resultStore.Get(simulationId);
            return result;
        }


        ///// Custom
        // TODO: Add a toggle/config//auth on here for more control

        [HttpGet("Mock")]
        public string MockSetup()
        {   
            var persons = _personStore.GetAllPersons();
            if (persons.Count() > 0) {
                throw new Exception("DB is populated. Cannot generate mocks.");
            }

            var mockPersons = MockCreator.GetMockPersons(100);
            _personStore.AddPersons(mockPersons);

            return "Mocks generated";
        }

    }
}
