using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Storage;

namespace sample_scenario
{
    public class SampleScenarioPersonCreationRequestHandler: IHandlePersonCreationRequests<SampleScenarioPersonRequest>
    {
        private readonly IStorePersons<SampleScenarioPerson> _personStore;

        public SampleScenarioPersonCreationRequestHandler(
            IStorePersons<SampleScenarioPerson> personStore) {
            _personStore = personStore;
        }
        public void Handle(IEnumerable<SampleScenarioPersonRequest> personsRequest) {
            var persons = personsRequest.Select(x => new SampleScenarioPerson() {
                Id = x.Id
            });

            _personStore.AddPersons(persons);
        }
    }
}