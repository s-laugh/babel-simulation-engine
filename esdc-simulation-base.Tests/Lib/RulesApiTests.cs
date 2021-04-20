using System;
using Microsoft.Extensions.Options;
using Xunit;
using FakeItEasy;
using RestSharp;

using esdc_simulation_base.Src.Rules;

namespace esdc_simulation_base.Tests.Lib
{
    public class RulesApiTests
    {
        
        [Fact]
        public void ShouldWorkNormally()
        {
            // Arrange
            decimal testAmount = 1.56m;

            var client = A.Fake<IRestClient>();
            var options = Options.Create(new RulesOptions() {
                Url = "http://localhost:6000"
            });

            var postResult = A.Fake<RestResponse<RulesResponse<decimal>>>();
            postResult.Data = new RulesResponse<decimal>(){ Amount = testAmount };
            postResult.StatusCode = System.Net.HttpStatusCode.OK;
            
            A.CallTo(() => client.Execute<RulesResponse<decimal>>(A<RestRequest>._, Method.POST))
                .Returns(postResult);

            // Act
            var sut = new RulesApi<IRulesRequest>(client, options);
            var rulesRequest = A.Fake<IRulesRequest>();
            var result = sut.Execute<decimal>("fake_endpoint", rulesRequest);

            // Assert
            Assert.Equal(testAmount, result);
        }


    }
}
