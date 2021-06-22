using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using EFModels = maternity_benefits.Storage.EF.Models;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace maternity_benefits.Storage.EF.Store
{
    public class MaternityBenefitsSimulationEFStore : IStoreSimulations<MaternityBenefitsCase>
    {
        private readonly ApplicationDbContext _context;

        public MaternityBenefitsSimulationEFStore(ApplicationDbContext context) {
            _context = context;
        }

        public void Save(Simulation<MaternityBenefitsCase> simulation) {
            var dbModel = ConvertToDb(simulation);
            _context.Simulations.Add(dbModel);
            _context.SaveChanges();
        }

        public Simulation<MaternityBenefitsCase> Get(Guid simulationId) {
            var dbModel = _context.Simulations
                .AsNoTracking()
                .Include(x => x.BaseCase)
                .Include(x => x.VariantCase)
                .First(x => x.Id == simulationId);
            return ConvertFromDb(dbModel);
        }

        // TODO: May want a soft-delete
        public void Delete(Guid simulationId) {
            var objectToRemove = _context.Simulations
                .AsNoTracking()
                .First(x => x.Id == simulationId);
            _context.Remove(objectToRemove);
            _context.SaveChanges();
        }

        public static EFModels.MaternityBenefitsSimulation ConvertToDb(Simulation<MaternityBenefitsCase> model) {
            return new EFModels.MaternityBenefitsSimulation() {
                Id = model.Id,
                SimulationName = model.Name,
                BaseCaseId = model.BaseCase.Id,
                BaseCase = ConvertToDb(model.BaseCase),
                VariantCaseId = model.VariantCase.Id,
                VariantCase = ConvertToDb(model.VariantCase),
                DateCreated = model.DateCreated,
            };
        }

        public static Simulation<MaternityBenefitsCase> ConvertFromDb(EFModels.MaternityBenefitsSimulation dbModel) {
            return new Simulation<MaternityBenefitsCase>() {
                Id = dbModel.Id,
                Name = dbModel.SimulationName,
                DateCreated = dbModel.DateCreated,
                BaseCase = ConvertFromDb(dbModel.BaseCase),
                VariantCase = ConvertFromDb(dbModel.VariantCase)
            };
        }

        public static EFModels.MaternityBenefitsSimulationCase ConvertToDb(MaternityBenefitsCase model) {
            return new EFModels.MaternityBenefitsSimulationCase() {
                Id = model.Id,
                MaxWeeklyAmount = model.MaxWeeklyAmount,
                NumWeeks = model.NumWeeks,
                Percentage = model.Percentage
            };
        }

        public static MaternityBenefitsCase ConvertFromDb(EFModels.MaternityBenefitsSimulationCase dbModel) {
            return new MaternityBenefitsCase() {
                Id = dbModel.Id,
                MaxWeeklyAmount = dbModel.MaxWeeklyAmount,
                NumWeeks = dbModel.NumWeeks,
                Percentage = dbModel.Percentage
            };
        }
        
    }
}
