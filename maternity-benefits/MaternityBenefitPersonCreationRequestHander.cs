using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Storage;

namespace maternity_benefits
{
    public class MaternityBenefitPersonCreationRequestHandler: IHandlePersonCreationRequests<MaternityBenefitsPersonRequest>
    {
        private readonly IStoreUnemploymentRegions _regionStore;
        private readonly IStorePersons<MaternityBenefitsPerson> _personStore;

        public MaternityBenefitPersonCreationRequestHandler(
            IStoreUnemploymentRegions regionStore,
            IStorePersons<MaternityBenefitsPerson> personStore) {
            _regionStore = regionStore;
            _personStore = personStore;
        }
        public void Handle(IEnumerable<MaternityBenefitsPersonRequest> personsRequest) {
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
    }
}