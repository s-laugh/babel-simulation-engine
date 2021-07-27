using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using EFModels = maternity_benefits.Storage.EF.Models;

using esdc_simulation_base.Src.Storage;
using esdc_simulation_base.Src.Classes;

namespace maternity_benefits.Storage.EF.Store
{
    public class MaternityBenefitsPersonEFStore : IStorePersons<MaternityBenefitsPerson>
    {
        private readonly ApplicationDbContext _context;

        public MaternityBenefitsPersonEFStore(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<MaternityBenefitsPerson> GetAllPersons() {
            return _context.Persons
                .AsNoTracking()
                .Select(x => ConvertFromDb(x))
                .ToList();
        }

        public void AddPersons(IEnumerable<MaternityBenefitsPerson> persons) {
            foreach (var person in persons) {
                var dbModel = ConvertToDb(person);
                _context.Persons.Add(dbModel);
            }

            _context.SaveChanges();
        }

        public void DeletePerson(Guid id) {
            var dbPerson = _context.Persons
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
            if (dbPerson == null) {
                throw new NotFoundException("Person not found");
            }
            _context.Persons.Remove(dbPerson);
            _context.SaveChanges();
        }

        public void Clear() {
            var results = _context.Persons.AsNoTracking();
            foreach (var result in results) {
                _context.Persons.Remove(result);
            }
            _context.SaveChanges();
        }


        public static MaternityBenefitsPerson ConvertFromDb(EFModels.MaternityBenefitsPerson dbModel) {
            return new MaternityBenefitsPerson() {
                Id = dbModel.Id,
                Age = dbModel.Age,
                AverageIncome = dbModel.AverageIncome,
                SpokenLanguage = dbModel.SpokenLanguage,
                EducationLevel = dbModel.EducationLevel,
                Province = dbModel.Province,
            };
        }

        public static EFModels.MaternityBenefitsPerson ConvertToDb(MaternityBenefitsPerson model) {
            return new EFModels.MaternityBenefitsPerson() {
                Id = model.Id,
                Age = model.Age,
                AverageIncome = model.AverageIncome,
                SpokenLanguage = model.SpokenLanguage,
                EducationLevel = model.EducationLevel,
                Province = model.Province,
            };
        }
    }
}
