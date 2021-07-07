using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Storage;

namespace maternity_benefits
{
    public class MaternityBenefitPersonCreationRequestHandler: IHandlePersonCreationRequests<MaternityBenefitsPersonRequest>
    {
        private readonly IStorePersons<MaternityBenefitsPerson> _personStore;

        public MaternityBenefitPersonCreationRequestHandler(
            IStorePersons<MaternityBenefitsPerson> personStore) {
            _personStore = personStore;
        }
        public void Handle(IEnumerable<MaternityBenefitsPersonRequest> personsRequest) {
            var persons = personsRequest.Select(x => new MaternityBenefitsPerson() {
                Id = Guid.NewGuid(),
                AverageIncome = x.AverageIncome,
                SpokenLanguage = x.SpokenLanguage,
                EducationLevel = x.EducationLevel,
                Province = x.Province,
                Age = x.Age
            });

            _personStore.AddPersons(persons);
        }
    }
}