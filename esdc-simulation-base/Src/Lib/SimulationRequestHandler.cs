using System;

using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace esdc_simulation_base.Src.Lib
{
    public class SimulationRequestHandler<T, U, V> : IHandleSimulationRequests<U>
        where T : ISimulationCase
        where U : ISimulationCaseRequest
        where V : IPerson
    {
        private readonly IBuildSimulations<T, U> _simulationBuilder;
        private readonly IStoreSimulations<T> _simulationStore;
        private readonly IStorePersons<V> _personStore;
        private readonly IStoreSimulationResults<T> _resultStore;
        private readonly IRunSimulations<T, V> _runner;

        public SimulationRequestHandler(
            IBuildSimulations<T, U> simulationBuilder,
            IStoreSimulations<T> simulationStore,
            IStorePersons<V> personStore,
            IStoreSimulationResults<T> resultStore,
            IRunSimulations<T, V> runner 
        ) {
            _simulationBuilder = simulationBuilder;
            _simulationStore = simulationStore;
            _personStore = personStore;
            _resultStore = resultStore;
            _runner = runner;
        }

        public Guid Handle(SimulationRequest<U> request) {
            var simulation = _simulationBuilder.Build(request);

            _simulationStore.Save(simulation);
            
            var persons = _personStore.GetAllPersons();
            var result = _runner.Run(simulation, persons);

            _resultStore.Save(simulation.Id, result);

            return simulation.Id;
        }
    }
}