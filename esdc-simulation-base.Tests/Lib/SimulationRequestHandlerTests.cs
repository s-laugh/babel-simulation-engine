using System;
using System.Collections.Generic;

using Xunit;
using FakeItEasy;

using esdc_simulation_base.Src.Lib;
using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;

namespace esdc_simulation_base.Tests.Lib
{
    public class SimulationRequestHandlerTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var simulationStore = A.Fake<IStoreSimulations<ISimulationCase>>();
            var personStore = A.Fake<IStorePersons<IPerson>>();
            var resultStore = A.Fake<IStoreSimulationResults<ISimulationCase>>();
            var runner = A.Fake<IRunSimulations<ISimulationCase, IPerson>>();
            var simulationBuilder = A.Fake<IBuildSimulations<ISimulationCase, ISimulationCaseRequest>>();

            var testId = Guid.NewGuid();
            var simulation = new Simulation<ISimulationCase>() {
                Id = testId
            };
            A.CallTo(() => simulationBuilder.Build(A<SimulationRequest<ISimulationCaseRequest>>._)).Returns(simulation);
            
            
            // Act
            var sut = new SimulationRequestHandler<
                ISimulationCase, 
                ISimulationCaseRequest,
                IPerson>
                (simulationBuilder, simulationStore, personStore, resultStore, runner);

            var request = new SimulationRequest<ISimulationCaseRequest>();
            
            var result = sut.Handle(request);


            // Assert
            A.CallTo(() => simulationBuilder.Build(A<SimulationRequest<ISimulationCaseRequest>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => simulationStore.Save(A<Simulation<ISimulationCase>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => personStore.GetAllPersons()).MustHaveHappenedOnceExactly();
            A.CallTo(() => runner.Run(A<Simulation<ISimulationCase>>._, A<IEnumerable<IPerson>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => resultStore.Save(testId, A<SimulationResult>._)).MustHaveHappenedOnceExactly();
            
            Assert.Equal(result, testId);
        }
    }
}
