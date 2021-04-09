using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using FakeItEasy;

namespace sample_scenario.Tests
{
    public class SampleScenarioExecutorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            decimal fakeResult = 0;
            
            // Act
            var sut = new SampleScenarioExecutor();

            var simulationCase = A.Fake<SampleScenarioCase>();
            var person = new SampleScenarioPerson();
            
            var result = sut.Execute(simulationCase, person);

            // Assert
            Assert.Equal(fakeResult, result);
        }
    }
}
