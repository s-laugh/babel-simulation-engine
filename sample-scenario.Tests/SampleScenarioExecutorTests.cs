using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using FakeItEasy;

using esdc_simulation_base.Src.Rules;
using sample_scenario.Rules;

namespace sample_scenario.Tests
{
    public class SampleScenarioExecutorTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            decimal fakeResult = 0;

            var rulesApi = A.Fake<IRulesEngine<SampleScenarioRulesRequest>>();
            
            // Act
            var sut = new SampleScenarioExecutor(rulesApi);

            var simulationCase = A.Fake<SampleScenarioCase>();
            var person = new SampleScenarioPerson();
            
            var result = sut.Execute(simulationCase, person);

            // Assert
            Assert.Equal(fakeResult, result);
        }
    }
}
