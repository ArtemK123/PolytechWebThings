using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class ThingsApiClient
    {
        private const string ApiUrlBase = "api/ThingsApi/";

        private readonly HttpClient httpClient;
        private readonly HttpResponseMessageParser httpResponseMessageParser;

        public ThingsApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
        {
            this.httpClient = httpClient;
            this.httpResponseMessageParser = httpResponseMessageParser;
        }

        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThingsAsync(GetWorkspaceWithThingsRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiUrlBase}GetWorkspaceWithThings")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult<GetWorkspaceWithThingsResponse>>(response);
        }
    }
}