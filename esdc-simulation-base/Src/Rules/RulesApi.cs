using System;
using Microsoft.Extensions.Options;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace esdc_simulation_base.Src.Rules
{
    public class RulesApi<T> : IRulesEngine<T>
        where T: IRulesRequest
    {
        private readonly IRestClient _client;

        public RulesApi(IRestClient client, IOptions<RulesOptions> optionsAccessor) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();
        }

        public W Execute<W>(string endpoint, T request) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(request);
            var result = _client.Post<RulesResponse<W>>(restRequest);

            if (result.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new RulesApiException(result.ErrorMessage);
            }
            return result.Data.Amount;
        }

    }
}