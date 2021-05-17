using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class WorkspaceApiClient
    {
        private const string ApiUrlBase = "api/WorkspaceApi/";

        private readonly HttpClient httpClient;
        private readonly HttpResponseMessageParser httpResponseMessageParser;

        public WorkspaceApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
        {
            this.httpClient = httpClient;
            this.httpResponseMessageParser = httpResponseMessageParser;
        }

        public async Task<OperationResult<GetUserWorkspacesResponse>> GetUserWorkspacesAsync()
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{ApiUrlBase}GetUserWorkspaces"));
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult<GetUserWorkspacesResponse>>(response);
        }

        public async Task<OperationResult<WorkspaceApiModel>> GetByIdAsync(GetWorkspaceByIdRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiUrlBase}GetById")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult<WorkspaceApiModel>>(response);
        }

        public async Task<OperationResult> CreateAsync(CreateWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiUrlBase}Create")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<OperationResult> UpdateAsync(UpdateWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(request: new HttpRequestMessage(HttpMethod.Post, $"{ApiUrlBase}Update")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<OperationResult> DeleteAsync(DeleteWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiUrlBase}Delete")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
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