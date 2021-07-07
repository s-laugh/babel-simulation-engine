using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using EFModels = maternity_benefits.Storage.EF.Models;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace maternity_benefits.Storage.EF.Store
{
    public class MaternityBenefitsSimulationResultsEFStore : IStoreSimulationResults<MaternityBenefitsCase>
    {
        private readonly ApplicationDbContext _context;

        public MaternityBenefitsSimulationResultsEFStore(ApplicationDbContext context) {
            _context = context;
        }

        public void Save(Guid simulationId, SimulationResult simulationResult) {
           var dbModel = ConvertToDb(simulationId, simulationResult);
           _context.SimulationResults.Add(dbModel);
           _context.SaveChanges();
        } 

        public SimulationResult Get(Guid simulationId) {
            var dbModel = _context.SimulationResults
                .AsNoTracking()
                .Include(x => x.PersonResults)
                .ThenInclude(x => x.Person)
                .First(x => x.SimulationId == simulationId);
            
            return ConvertFromDb(dbModel);
        }

        public void Delete(Guid simulationId) {
           var objectToRemove = _context.SimulationResults
                .AsNoTracking()
                .First(x => x.SimulationId == simulationId);
            _context.Remove(objectToRemove);
            _context.SaveChanges();
        }

        public EFModels.MaternityBenefitsSimulationResult ConvertToDb(Guid simulationId, SimulationResult model) {
            return new EFModels.MaternityBenefitsSimulationResult() {
                Id = Guid.NewGuid(),
                SimulationId = simulationId,
                PersonResults = model.PersonResults.Select(x => ConvertToDb(x)).ToList()
            };
        }

        public SimulationResult ConvertFromDb(EFModels.MaternityBenefitsSimulationResult dbModel) {
            return new SimulationResult() {
                PersonResults = dbModel.PersonResults.Select(x => ConvertFromDb(x)).ToList()
            };
        }

        public EFModels.MaternityBenefitsPersonResult ConvertToDb(PersonResult model) {
            return new EFModels.MaternityBenefitsPersonResult() {
                PersonId = model.Person.Id,
                //MaternityBenefitsPerson = MaternityBenefitsPersonEFStore.ConvertToDb((MaternityBenefitsPerson)model.Person),
                BaseAmount = model.BaseAmount,
                VariantAmount = model.VariantAmount
            };
        }

        public PersonResult ConvertFromDb(EFModels.MaternityBenefitsPersonResult dbModel) {
            return new PersonResult() {
                BaseAmount = dbModel.BaseAmount,
                VariantAmount = dbModel.VariantAmount,
                Person = MaternityBenefitsPersonEFStore.ConvertFromDb(dbModel.Person)
            };
        }
    }
}
