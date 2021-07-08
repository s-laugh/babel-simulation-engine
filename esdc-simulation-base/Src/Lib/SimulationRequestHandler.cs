using System;

using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace esdc_simulation_base.Src.Lib
{
    public class SimulationRequestHandler<T, U> : IHandleSimulationRequests<T>
        where T : ISimulationCase
        where U : IPerson
    {
        private readonly IStoreSimulations<T> _simulationStore;
        private readonly IStorePersons<U> _personStore;
        private readonly IStoreSimulationResults<T> _resultStore;
        private readonly IRunSimulations<T, U> _runner;

        public SimulationRequestHandler(
            IStoreSimulations<T> simulationStore,
            IStorePersons<U> personStore,
            IStoreSimulationResults<T> resultStore,
            IRunSimulations<T, U> runner 
        ) {
            _simulationStore = simulationStore;
            _personStore = personStore;
            _resultStore = resultStore;
            _runner = runner;
        }

        public Guid Handle(Simulation<T> simulation) {
            _simulationStore.Save(simulation);
            
            var persons = _personStore.GetAllPersons();
            var result = _runner.Run(simulation, persons);

            _resultStore.Save(simulation.Id, result);

            return simulation.Id;
        }
    }
}