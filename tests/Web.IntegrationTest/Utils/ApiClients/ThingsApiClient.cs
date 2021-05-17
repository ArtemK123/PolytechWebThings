using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Things.Request;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class ThingsApiClient
    {
        private const string ApiBaseUrl = "api/ThingsApi/";
        private readonly HttpClient httpClient;
        private readonly HttpResponseMessageParser httpResponseMessageParser;

        public ThingsApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
        {
            this.httpClient = httpClient;
            this.httpResponseMessageParser = httpResponseMessageParser;
        }

        public async Task<OperationResult> ChangePropertyStateAsync(ChangePropertyStateRequest request)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "ChangePropertyState")
            {
                Content = JsonContent.Create(request)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }
    }
}