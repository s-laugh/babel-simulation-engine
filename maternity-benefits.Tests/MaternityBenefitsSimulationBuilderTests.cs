using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Classes;

using esdc_simulation_classes.MaternityBenefits;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsSimulationBuilderTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var testName = "Simulation Name";

            // Act
            var sut = new MaternityBenefitsSimulationBuilder();

            var req = new SimulationRequest<MaternityBenefitsCaseRequest>() {
                VariantCaseRequest = new MaternityBenefitsCaseRequest() {
                    NumWeeks = 1,
                    MaxWeeklyAmount = 3,
                    Percentage = 50
                },
                BaseCaseRequest = new MaternityBenefitsCaseRequest() {
                    NumWeeks = 2,
                    MaxWeeklyAmount = 6,
                    Percentage = 100
                }, 
                SimulationName = testName
            };
            
            var result = sut.Build(req);

            // Assert
            Assert.Equal(testName, result.Name);
        }
    }
}
