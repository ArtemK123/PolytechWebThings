using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class WorkspaceApiClient : ApiClientBase
    {
        public WorkspaceApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
            : base(httpClient, httpResponseMessageParser)
        {
        }

        protected override string ApiBaseUrl => "api/WorkspaceApi/";

        public async Task<OperationResult<GetUserWorkspacesResponse>> GetUserWorkspacesAsync()
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}GetUserWorkspaces"));
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<GetUserWorkspacesResponse>>(response);
        }

        public async Task<OperationResult<WorkspaceApiModel>> GetByIdAsync(GetWorkspaceByIdRequest requestModel)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}GetById")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<WorkspaceApiModel>>(response);
        }

        public async Task<OperationResult> CreateAsync(CreateWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}Create")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<OperationResult> UpdateAsync(UpdateWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(request: new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}Update")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<OperationResult> DeleteAsync(DeleteWorkspaceRequest requestModel)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}Delete")
            {
                Content = JsonContent.Create(requestModel)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }
    }
}