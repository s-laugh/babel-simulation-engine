using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Storage;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitPersonCreationRequestHandlerTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var regionStore = A.Fake<IStoreUnemploymentRegions>();
            var personStore = A.Fake<IStorePersons<MaternityBenefitsPerson>>();

            var testRegionId = Guid.NewGuid();

            var fakeRegion = new UnemploymentRegion() {
                Id = testRegionId
            };

            var fakeRegions = new List<UnemploymentRegion>() { fakeRegion };
            A.CallTo(() => regionStore.GetUnemploymentRegions()).Returns(fakeRegions);
            
            
            // Act
            var sut = new MaternityBenefitPersonCreationRequestHandler(regionStore, personStore);

            var request = new List<MaternityBenefitsPersonRequest>() {
                new MaternityBenefitsPersonRequest() {
                    UnemploymentRegionId = testRegionId,
                    WeeklyIncome = new List<WeeklyIncome>() {
                        new WeeklyIncome() {
                            StartDate = DateTime.Now,
                            Income = 100
                        }
                    }
                }
            };
            
            sut.Handle(request);

            // Assert
            A.CallTo(() => regionStore.GetUnemploymentRegions()).MustHaveHappenedOnceExactly();
            A.CallTo(() => personStore.AddPersons(A<IEnumerable<MaternityBenefitsPerson>>._)).MustHaveHappenedOnceExactly();
        }
    }
}
