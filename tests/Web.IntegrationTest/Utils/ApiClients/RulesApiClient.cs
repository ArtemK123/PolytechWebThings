using System.Net.Http;
using Web.IntegrationTest.Utils.Parsers;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class RulesApiClient : ApiClientBase
    {
        public RulesApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
            : base(httpClient, httpResponseMessageParser)
        {
        }

        protected override string ApiBaseUrl => "api/RuleApi/";
    }
}