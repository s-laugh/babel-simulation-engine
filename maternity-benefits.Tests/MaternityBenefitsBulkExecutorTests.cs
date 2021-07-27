using System;
using System.Collections.Generic;
using Xunit;

using FakeItEasy;

using esdc_simulation_base.Src.Rules;
using Rule = esdc_rules_classes.MaternityBenefits;

namespace maternity_benefits.Tests
{
    public class MaternityBenefitsBulkExecutorTests
    {
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            var rulesApi = A.Fake<IRulesEngine>();
            var dict = new Dictionary<Guid, Rule.MaternityBenefitsResponse>() {
                {
                    Guid.NewGuid(),
                    new Rule.MaternityBenefitsResponse() {
                        Amount = 1000
                    }
                },
                {
                    Guid.NewGuid(),
                    new Rule.MaternityBenefitsResponse() {
                        Amount = 1000
                    }
                }
            };
            var testResult = new Rule.MaternityBenefitsBulkResponse() { ResponseDict = dict };

            A.CallTo(() => rulesApi.Execute<Rule.MaternityBenefitsBulkResponse>("MaternityBenefits/Bulk", A<Rule.MaternityBenefitsBulkRequest>._))
                .Returns(testResult);
            
            // Act
            var sut = new MaternityBenefitsBulkExecutor(rulesApi);
            var simCase = A.Fake<MaternityBenefitsCase>();
            var persons = new List<MaternityBenefitsPerson>() {
                A.Fake<MaternityBenefitsPerson>()
            };
            var result = sut.Execute(simCase, persons);

            // Assert
            A.CallTo(() => rulesApi.Execute<Rule.MaternityBenefitsBulkResponse>("MaternityBenefits/Bulk", A<Rule.MaternityBenefitsBulkRequest>._))
                .MustHaveHappenedOnceExactly();
            Assert.Equal(2, testResult.ResponseDict.Count);
        }
    }
}
