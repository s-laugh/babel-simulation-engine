using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Rules;
using esdc_simulation_base.Src.Storage;
using maternity_benefits.Rules;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsExecutorTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var rulesApi = A.Fake<IRulesEngine<MaternityBenefitsRulesRequest>>();
            decimal testResult = 1.67m;
            A.CallTo(() => rulesApi.Execute<decimal>("MaternityBenefits", A<MaternityBenefitsRulesRequest>._)).Returns(testResult);
            
            // Act
            var sut = new MaternityBenefitsExecutor(rulesApi);
            var simCase = A.Fake<MaternityBenefitsCase>();
            var person = A.Fake<MaternityBenefitsPerson>();
            
            var result = sut.Execute(simCase, person);

            // Assert
            Assert.Equal(testResult, result);
        }
    }
}
