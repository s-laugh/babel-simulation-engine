using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Storage;

namespace sample_scenario.Tests
{
    public class SampleScenarioPersonCreationRequestHandlerTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var personStore = A.Fake<IStorePersons<SampleScenarioPerson>>();

            // Act
            var sut = new SampleScenarioPersonCreationRequestHandler(personStore);

            var request = new List<SampleScenarioPersonRequest>() {
                new SampleScenarioPersonRequest() {
                    Id = Guid.NewGuid()
                }
            };
            
            sut.Handle(request);

            // Assert
            A.CallTo(() => personStore.AddPersons(A<IEnumerable<SampleScenarioPerson>>._)).MustHaveHappenedOnceExactly();
        }
    }
}
