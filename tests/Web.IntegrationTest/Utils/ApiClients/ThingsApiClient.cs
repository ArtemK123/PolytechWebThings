using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Models.Things.Response;
using Web.Models.Workspace.Response;

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

        public async Task<OperationResult<ThingStateApiModel>> GetThingStateAsync(GetThingStateRequest request)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "GetThingState")
            {
                Content = JsonContent.Create(request)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult<ThingStateApiModel>>(response);
        }

        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThingsAsync(GetWorkspaceWithThingsRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}GetWorkspaceWithThings")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult<GetWorkspaceWithThingsResponse>>(response);
        }
    }
}