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

namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleSimulationRequests<MaternityBenefitsCaseRequest> _requestHandler;
        private readonly IStorePersons<MaternityBenefitsPerson> _personStore;
        private readonly IStoreSimulations<MaternityBenefitsCase> _simulationStore;
        private readonly IStoreSimulationResults<MaternityBenefitsCase> _resultStore;
        private readonly IStoreUnemploymentRegions _regionStore;

        public MaternityBenefitsController(
            IHandleSimulationRequests<MaternityBenefitsCaseRequest> requestHandler,
            IStoreSimulations<MaternityBenefitsCase> simulationStore,
            IStorePersons<MaternityBenefitsPerson> personStore,
            IStoreSimulationResults<MaternityBenefitsCase> resultStore,
            IStoreUnemploymentRegions regionStore
        )
        {
            _requestHandler = requestHandler;
            _simulationStore = simulationStore;
            _personStore = personStore;
            _resultStore = resultStore;
            _regionStore = regionStore;
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
            var persons = _personStore.GetAllPersons();
            return persons;
        }

        // TODO: This may end up being too big a response. Maybe a summary?
        [HttpPost("Persons")]
        public void AddPersons(List<MaternityBenefitsPersonRequest> personsRequest)
        {
            // TODO: Maybe this should go in a separate handler as well
            var unemploymentRegions = _regionStore.GetUnemploymentRegions();
            var regionDict = unemploymentRegions.ToDictionary(x => x.Id);

            var persons = personsRequest.Select(x => new MaternityBenefitsPerson() {
                Id = Guid.NewGuid(),
                WeeklyIncome = x.WeeklyIncome,
                UnemploymentRegion = regionDict[x.UnemploymentRegionId],
                Flsah = x.Flsah,
                Age = x.Age
            });

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


        ///// Custom

        // TODO: May need a POST here as well
        [HttpGet("UnemploymentRegions")]
        public IEnumerable<UnemploymentRegion> GetUnemploymentRegions()
        {
            var regions = _regionStore.GetUnemploymentRegions();
            return regions;
        }

        [HttpGet("Mock")]
        public string MockSetup()
        {   
            _personStore.Clear();
            _regionStore.Clear();

            var regions = MockCreator.GetUnemploymentRegions();
            _regionStore.AddUnemploymentRegions(regions);

            var persons = MockCreator.GeneratePersons(regions.ToList(), 100);
            _personStore.AddPersons(persons);

            return "OK";
        }

    }
}
