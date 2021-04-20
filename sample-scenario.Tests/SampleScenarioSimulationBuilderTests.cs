using System;
using Xunit;

using esdc_simulation_base.Src.Classes;

namespace sample_scenario.Tests
{
    public class SampleScenarioSimulationBuilderTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Act
            var sut = new SampleScenarioSimulationBuilder();

            var req = new SimulationRequest<SampleScenarioCaseRequest>() {
                VariantCaseRequest = new SampleScenarioCaseRequest() {
                    SomeFactor = 3,
                    SomeToggle =false,
                    SomeThreshold = 3.44,
                },
                BaseCaseRequest = new SampleScenarioCaseRequest() {
                     SomeFactor = 5,
                    SomeToggle = true,
                    SomeThreshold = 3.14,
                }, 
                SimulationName = "Test Simulation"
            };
            
            var result = sut.Build(req);

            // Assert
            Assert.Equal(5, result.BaseCase.SomeFactor);
            Assert.Equal(3.44, result.VariantCase.SomeThreshold);
            Assert.Equal("Test Simulation", result.Name);
        }
    }
}
