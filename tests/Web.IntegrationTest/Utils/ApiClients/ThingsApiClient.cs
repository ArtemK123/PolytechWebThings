using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Models.Things.Response;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class ThingsApiClient : ApiClientBase
    {
        public ThingsApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
            : base(httpClient, httpResponseMessageParser)
        {
        }

        protected override string ApiBaseUrl => "api/ThingsApi/";

        public async Task<OperationResult> ChangePropertyStateAsync(ChangePropertyStateRequest request)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "ChangePropertyState")
            {
                Content = JsonContent.Create(request)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<OperationResult<ThingStateApiModel>> GetThingStateAsync(GetThingStateRequest request)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "GetThingState")
            {
                Content = JsonContent.Create(request)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<ThingStateApiModel>>(response);
        }

        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThingsAsync(GetWorkspaceWithThingsRequest requestModel)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}GetWorkspaceWithThings")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<GetWorkspaceWithThingsResponse>>(response);
        }
    }
}