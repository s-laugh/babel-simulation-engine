using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Storage;
using esdc_simulation_classes.MaternityBenefits;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitPersonCreationRequestHandlerTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var personStore = A.Fake<IStorePersons<MaternityBenefitsPerson>>();
            
            // Act
            var sut = new MaternityBenefitPersonCreationRequestHandler(personStore);
            var request = new List<MaternityBenefitsPersonRequest>() {
                new MaternityBenefitsPersonRequest() {
                    AverageIncome = 1000
                }
            };
            sut.Handle(request);

            // Assert
            A.CallTo(() => personStore.AddPersons(A<IEnumerable<MaternityBenefitsPerson>>._)).MustHaveHappenedOnceExactly();
        }
    }
}
