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

        public async Task<HttpResponseMessage> CreateWorkspaceAsync(CreateWorkspaceRequest requestModel)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/WorkspaceApi/Create")
            {
                Content = JsonContent.Create(requestModel)
            });
        }

        public async Task<HttpResponseMessage> DeleteWorkspaceAsync(DeleteWorkspaceRequest requestModel)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/WorkspaceApi/Delete")
            {
                Content = JsonContent.Create(requestModel)
            });
        }
    }
}