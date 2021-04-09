using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public class CaseRunner<T, U> : IRunCases<T, U>
        where T : ISimulationCase
        where U : IPerson
    {
        private readonly IExecuteRules<T, U> _executor;

        public CaseRunner(IExecuteRules<T, U> executor) {
            _executor = executor;
        }

        public SimulationCaseResult Run(T simulationCase, IEnumerable<U> persons) {
            var result = new SimulationCaseResult();
            var personDict = persons.ToDictionary(x => x.Id);

            foreach (var person in persons) {
                var amount = _executor.Execute(simulationCase, person);
                var nextResult = new PersonCaseResult() {
                    Person = personDict[person.Id],
                    Amount = amount
                };
                result.ResultSet.Add(person.Id, nextResult);
            }

            return result;
        }
    }
}