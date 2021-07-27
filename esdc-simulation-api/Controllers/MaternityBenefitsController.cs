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
        public ActionResult<SimulationResponse> GetSimulation(Guid simulationId)
        {
            try {
                var simulation = _simulationStore.Get(simulationId);
                return Convert(simulation);
            } catch (NotFoundException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<AllSimulationsResponse> GetAllSimulations()
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
        public ActionResult<CreateSimulationResponse> CreateSimulation(CreateSimulationRequest request)
        {
            var simulation = Convert(request);
            var simulationId = _requestHandler.Handle(simulation);
            return new CreateSimulationResponse {
                Id = simulationId
            };
        }

        [HttpGet("{simulationId}/Results")]
        public ActionResult<FullResponse> GetFullResponse(Guid simulationId)
        {
            try {
                var simulation = _simulationStore.Get(simulationId);
                var result = _resultStore.Get(simulationId);
                
                return new FullResponse() {
                    Simulation = Convert(simulation),
                    Result = Convert(result)
                };
            }
            catch (NotFoundException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{untilLastXDays}/Batch")]
        public ActionResult DeleteSimulationBatch(int untilLastXDays)
        {
            var sims = _simulationStore.GetAll();
            var simsToDelete = sims.Where(x => x.DateCreated < DateTime.Now.AddDays(-untilLastXDays));
            foreach (var sim in simsToDelete) {
                _simulationStore.Delete(sim.Id);
            }
            return Ok();
        }

        [HttpDelete("{simulationId}")]
        public ActionResult DeleteSimulation(Guid simulationId)
        {
            try {
                _simulationStore.Delete(simulationId);
                return Ok();
            }
            catch (NotFoundException e) {
                return BadRequest(e.Message);
            }
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
                BaseCase = Convert(request.BaseCaseRequest),
                VariantCase = Convert(request.VariantCaseRequest)
            };
        }

        private MaternityBenefitsCase Convert(CaseRequest caseModel) {
            return new MaternityBenefitsCase() {
                Id = Guid.NewGuid(),
                NumWeeks = caseModel.NumWeeks,
                MaxWeeklyAmount = caseModel.MaxWeeklyAmount,
                Percentage = caseModel.Percentage
            };
        }

        private CaseRequest Convert(MaternityBenefitsCase caseModel) {
            return new CaseRequest() {
                NumWeeks = caseModel.NumWeeks,
                MaxWeeklyAmount = caseModel.MaxWeeklyAmount,
                Percentage = caseModel.Percentage
            };
        }
    }
}
