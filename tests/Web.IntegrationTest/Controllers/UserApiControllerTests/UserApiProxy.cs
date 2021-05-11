using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.UserApiControllerTests
{
    internal class UserApiProxy
    {
        private const string UserApiBaseUrl = "api/UserApi/";
        private readonly HttpClient httpClient;

        public UserApiProxy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<OperationResult> CreateAsync(CreateUserRequest createUserRequest)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(createUserRequest)
            });
            return await ParseResponseAsync<OperationResult>(response);
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginUserRequest loginUserRequest)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Login")
            {
                Content = JsonContent.Create(loginUserRequest)
            });
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Logout"));
        }

        private async Task<TOperationResult> ParseResponseAsync<TOperationResult>(HttpResponseMessage response)
            where TOperationResult : OperationResult
        {
            await using Stream responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TOperationResult>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}