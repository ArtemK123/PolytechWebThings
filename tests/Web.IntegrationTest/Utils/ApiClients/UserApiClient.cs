using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class UserApiClient
    {
        private const string UserApiBaseUrl = "api/UserApi/";
        private readonly HttpClient httpClient;
        private readonly HttpResponseMessageParser httpResponseMessageParser;

        public UserApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
        {
            this.httpClient = httpClient;
            this.httpResponseMessageParser = httpResponseMessageParser;
        }

        public async Task<OperationResult> CreateAsync(CreateUserRequest createUserRequest)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(createUserRequest)
            });
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<HttpResponseMessage> LoginRawResponseAsync(LoginUserRequest loginUserRequest)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Login")
            {
                Content = JsonContent.Create(loginUserRequest)
            });
        }

        public async Task<OperationResult> LoginAsync(LoginUserRequest loginUserRequest)
        {
            HttpResponseMessage response = await LoginRawResponseAsync(loginUserRequest);
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<HttpResponseMessage> LogoutRawResponseAsync()
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Logout"));
        }

        public async Task<OperationResult> LogoutAsync()
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Logout"));
            return await httpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }
    }
}