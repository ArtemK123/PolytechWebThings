using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

        public async Task<HttpResponseMessage> CreateAsync(CreateUserRequest createUserRequest)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(createUserRequest)
            });
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
    }
}