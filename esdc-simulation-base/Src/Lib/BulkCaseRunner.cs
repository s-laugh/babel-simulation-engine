using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public class BulkCaseRunner<T, U> : IRunCases<T, U>
        where T : ISimulationCase
        where U : IPerson
    {
        private readonly IExecuteBulkRules<T, U> _executor;
        private readonly int CHUNK_SIZE = 100;

        public BulkCaseRunner(IExecuteBulkRules<T, U> executor) {
            _executor = executor;
        }

        public SimulationCaseResult Run(T simulationCase, IEnumerable<U> persons) {
            var resultDict = new Dictionary<Guid, PersonCaseResult>();
            var personDict = persons.ToDictionary(x => x.Id);
            
            int i = 0;
            bool trigger = true;

            while (trigger) {
                var personBatch = persons.Skip(i*CHUNK_SIZE).Take(CHUNK_SIZE);
                if (personBatch.Count() == 0) { break; }
                i++;

                var nextSet = _executor.Execute(simulationCase, personBatch);

                foreach (var res in nextSet) {
                    var nextResult = new PersonCaseResult() {
                        Person = personDict[res.Key],
                        Amount = res.Value
                    };
                    resultDict.Add(res.Key, nextResult);
                }
            }

            return new SimulationCaseResult() {
                ResultSet = resultDict
            };
        }
    }
}