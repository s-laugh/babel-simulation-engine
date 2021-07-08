using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using maternity_benefits;

using esdc_simulation_classes.MaternityBenefits;


namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleSimulationRequests<MaternityBenefitsCase> _requestHandler;
        private readonly IStoreSimulations<MaternityBenefitsCase> _simulationStore;
        private readonly IStoreSimulationResults<MaternityBenefitsCase> _resultStore;

        public MaternityBenefitsController(
            IHandleSimulationRequests<MaternityBenefitsCase> requestHandler,
            IStoreSimulations<MaternityBenefitsCase> simulationStore,
            IStoreSimulationResults<MaternityBenefitsCase> resultStore
        )
        {
            _requestHandler = requestHandler;
            _simulationStore = simulationStore;
            _resultStore = resultStore;
        }

        [HttpGet("{simulationId}")]
        public SimulationResponse GetSimulation(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            return Convert(simulation);
        }

        [HttpGet]
        public AllSimulationsResponse GetAllSimulations()
        {
            var result = new List<SimulationResponse>();
            var simulations = _simulationStore.GetAll();
            foreach (var sim in simulations) {
                result.Add(Convert(sim));
            }
            return new AllSimulationsResponse() {
                Simulations = result
            };
        }

        [HttpPost]
        public CreateSimulationResponse CreateSimulation(CreateSimulationRequest request)
        {
            var simulation = Convert(request);
            var simulationId = _requestHandler.Handle(simulation);
            return new CreateSimulationResponse {
                Id = simulationId
            };
        }

        [HttpGet("{simulationId}/Results")]
        public FullResponse GetFullResponse(Guid simulationId)
        {
            var simulation = _simulationStore.Get(simulationId);
            var result = _resultStore.Get(simulationId);

            return new FullResponse() {
                Simulation = Convert(simulation),
                Result = Convert(result)
            };
        }

        [HttpDelete("{simulationId}")]
        public void DeleteSimulation(Guid simulationId)
        {
            _simulationStore.Delete(simulationId);
        }

        private SimulationResponse Convert(Simulation<MaternityBenefitsCase> simulation) {
            return new SimulationResponse() {
                Id = simulation.Id,
                SimulationName = simulation.Name,
                DateCreated = simulation.DateCreated,
                BaseCase = Convert(simulation.BaseCase),
                VariantCase = Convert(simulation.VariantCase)
            };
        }

        private CaseRequest Convert(MaternityBenefitsCase caseModel) {
            return new CaseRequest() {
                NumWeeks = caseModel.NumWeeks,
                MaxWeeklyAmount = caseModel.MaxWeeklyAmount,
                Percentage = caseModel.Percentage
            };
        }

        private SimulationResultResponse Convert(SimulationResult result) 
        {
            var personResults = result.PersonResults.Select(x => {
                return new PersonResultResponse() {
                    VariantAmount = x.VariantAmount,
                    BaseAmount = x.BaseAmount,
                    Person = Convert((MaternityBenefitsPerson)x.Person)
                };
            }).ToList();

            return new SimulationResultResponse() {
                PersonResults = personResults
            };
        }

        private PersonResponse Convert(MaternityBenefitsPerson person) {
            return new PersonResponse() {
                Id = person.Id,
                AverageIncome = person.AverageIncome,
                Age = person.Age,
                EducationLevel = person.EducationLevel,
                Province = person.Province,
                SpokenLanguage = person.SpokenLanguage
            };
        }

        private Simulation<MaternityBenefitsCase> Convert(CreateSimulationRequest request) {
            return new Simulation<MaternityBenefitsCase>() {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                Name = request.SimulationName,
                BaseCase = (MaternityBenefitsCase)request.BaseCaseRequest,
                VariantCase = (MaternityBenefitsCase)request.VariantCaseRequest
            };
        }
    }
}
