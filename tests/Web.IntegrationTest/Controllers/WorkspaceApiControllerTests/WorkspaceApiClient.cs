using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal class WorkspaceApiClient
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient httpClient;

        public WorkspaceApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetUserWorkspacesHttpResponseAsync()
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/WorkspaceApi/GetUserWorkspaces"));
        }

        public async Task<GetUserWorkspacesResponse> GetUserWorkspacesParsedResponseAsync()
        {
            HttpResponseMessage response = await GetUserWorkspacesHttpResponseAsync();
            string responseText = await response.Content.ReadAsStringAsync();
            GetUserWorkspacesResponse responseData = JsonSerializer.Deserialize<GetUserWorkspacesResponse>(responseText, jsonSerializerOptions) ?? throw new NullReferenceException();
            return responseData;
        }

        public async Task<HttpResponseMessage> GetByIdHttpResponseAsync(GetWorkspaceByIdRequest requestModel)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"api/WorkspaceApi/GetById/{requestModel.WorkspaceId}"));
        }

        public async Task<WorkspaceApiModel> GetByIdParsedResponseAsync(GetWorkspaceByIdRequest requestModel)
        {
            HttpResponseMessage response = await GetByIdHttpResponseAsync(requestModel);
            string responseText = await response.Content.ReadAsStringAsync();
            WorkspaceApiModel responseData = JsonSerializer.Deserialize<WorkspaceApiModel>(responseText, jsonSerializerOptions) ?? throw new NullReferenceException();
            return responseData;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateWorkspaceRequest requestModel)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/WorkspaceApi/Create")
            {
                Content = JsonContent.Create(requestModel)
            });
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateWorkspaceRequest requestModel)
        {
            return await httpClient.SendAsync(request: new HttpRequestMessage(HttpMethod.Post, $"api/WorkspaceApi/Update")
            {
                Content = JsonContent.Create(requestModel)
            });
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteWorkspaceRequest requestModel)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"api/WorkspaceApi/Delete/{requestModel.WorkspaceId}"));
        }
    }
}