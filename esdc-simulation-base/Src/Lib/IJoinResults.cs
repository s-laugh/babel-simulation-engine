using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IJoinResults
    {
         List<PersonResult> Join(SimulationCaseResult baseResult, SimulationCaseResult variantResult);
    }
}