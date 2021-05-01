using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;

namespace Web.IntegrationTest.Controllers.UserApi
{
    internal class UserApiProxy
    {
        private const string UserApiBaseUrl = "api/UserApi/";
        private readonly HttpClient httpClient;

        public UserApiProxy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateUserCommand createUserCommand)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(createUserCommand)
            });
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginUserCommand loginUserCommand)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Login")
            {
                Content = JsonContent.Create(loginUserCommand)
            });
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, UserApiBaseUrl + "Logout"));
        }
    }
}