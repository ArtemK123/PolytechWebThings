using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class UserApiClient : ApiClientBase
    {
        public UserApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
            : base(httpClient, httpResponseMessageParser)
        {
        }

        protected override string ApiBaseUrl => "api/UserApi/";

        public async Task<OperationResult> CreateAsync(CreateUserRequest createUserRequest)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(createUserRequest)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<HttpResponseMessage> LoginRawResponseAsync(LoginUserRequest loginUserRequest)
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "Login")
            {
                Content = JsonContent.Create(loginUserRequest)
            });
        }

        public async Task<OperationResult> LoginAsync(LoginUserRequest loginUserRequest)
        {
            HttpResponseMessage response = await LoginRawResponseAsync(loginUserRequest);
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }

        public async Task<HttpResponseMessage> LogoutRawResponseAsync()
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "Logout"));
        }

        public async Task<OperationResult> LogoutAsync()
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "Logout"));
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult>(response);
        }
    }
}