using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using sample_scenario;


namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleScenarioController : ControllerBase
    {
        private readonly IHandleSimulationRequests<SampleScenarioCaseRequest> _requestHandler;
        private readonly IStoreSimulations<SampleScenarioCase> _simulationStore;
        private readonly IStorePersons<SampleScenarioPerson> _personStore;
        private readonly IStoreSimulationResults<SampleScenarioCase> _resultStore;

        public SampleScenarioController(
            IHandleSimulationRequests<SampleScenarioCaseRequest> requestHandler,
            IStoreSimulations<SampleScenarioCase> simulationStore,
            IStorePersons<SampleScenarioPerson> personStore,
            IStoreSimulationResults<SampleScenarioCase> resultStore
        )
        {
            _requestHandler = requestHandler;
            _simulationStore = simulationStore;
            _personStore = personStore;
            _resultStore = resultStore;
        }

        [HttpGet("{simulationId}")]
        public Simulation<SampleScenarioCase> GetSimulation(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            return simulation;
        }

        [HttpPost]
        public CreateSimulationResponse CreateSimulation(SimulationRequest<SampleScenarioCaseRequest> request)
        {
            var simulationId = _requestHandler.Handle(request);
            return new CreateSimulationResponse {
                Id = simulationId
            };
        }

        [HttpGet("Persons")]
        public IEnumerable<SampleScenarioPerson> GetPersons()
        {
            var persons = _personStore.GetAllPersons();
            return persons;
        }

        [HttpPost("Persons")]
        public void AddPersons(List<SampleScenarioPerson> persons)
        {
            // TODO: Like with MB, may want person request
            // And then it uses a separate handler
            _personStore.AddPersons(persons);
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
    }
}
