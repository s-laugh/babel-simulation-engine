using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Storage;

namespace sample_scenario
{
    public class SampleScenarioPersonStore : IStorePersons<SampleScenarioPerson>
    {
        public IEnumerable<SampleScenarioPerson> GetAllPersons() {
            return new List<SampleScenarioPerson>();
        }
    }
}
