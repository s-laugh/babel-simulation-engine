using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Rules;
using esdc_simulation_base.Src.Storage;
using Rule = esdc_rules_classes.MaternityBenefits;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsExecutorTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var rulesApi = A.Fake<IRulesEngine>();
            var testResult = new Rule.MaternityBenefitsResponse() { Amount = 1.67m };
            A.CallTo(() => rulesApi.Execute<Rule.MaternityBenefitsResponse>("MaternityBenefits", A<Rule.MaternityBenefitsRequest>._)).Returns(testResult);
            
            // Act
            var sut = new MaternityBenefitsExecutor(rulesApi);
            var simCase = A.Fake<MaternityBenefitsCase>();
            var person = A.Fake<MaternityBenefitsPerson>();
            
            var result = sut.Execute(simCase, person);

            // Assert
            Assert.Equal(testResult.Amount, result);
        }
    }
}
