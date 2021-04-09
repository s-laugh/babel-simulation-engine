using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public class SimulationRunner<T, U> : IRunSimulations<T, U>
        where T : ISimulationCase
        where U : IPerson
    {
        private readonly IRunCases<T, U> _caseRunner;
        private readonly IJoinResults _joiner;

        public SimulationRunner(
            IRunCases<T, U> caseRunner,
            IJoinResults joiner
        ) {
            _caseRunner = caseRunner;
            _joiner = joiner;
        }

        public SimulationResult Run(Simulation<T> simulation, IEnumerable<U> persons) {
            // Run base case
            var baseResult = _caseRunner.Run(simulation.BaseCase, persons);

            // Run variant case
            var variantResult = _caseRunner.Run(simulation.VariantCase, persons);

            // Join Results
            var personResults = _joiner.Join(baseResult, variantResult);

            return new SimulationResult() {
                PersonResults = personResults
            };
        }
    }
}