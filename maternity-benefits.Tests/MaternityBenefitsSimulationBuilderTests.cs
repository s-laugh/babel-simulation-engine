using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Classes;
using esdc_simulation_base.Src.Storage;
using maternity_benefits.Rules;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsSimulationBuilderTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var regionStore = A.Fake<IStoreUnemploymentRegions>();
            var testRegionId = Guid.NewGuid();

            var fakeRegion = new UnemploymentRegion() {
                Id = testRegionId,
                Name = "Test Region"
            };

            var fakeRegions = new List<UnemploymentRegion>() { fakeRegion };
            A.CallTo(() => regionStore.GetUnemploymentRegions()).Returns(fakeRegions);

            // Act
            var sut = new MaternityBenefitsSimulationBuilder(regionStore);

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
                SimulationName = "Test Simulation"
            };
            
            var result = sut.Build(req);

            // Assert
            Assert.Equal("Test Region", result.BaseCase.RegionDict[testRegionId].Name);
            Assert.Equal("Test Region", result.VariantCase.RegionDict[testRegionId].Name);
            Assert.Equal("Test Simulation", result.Name);
        }
    }
}
