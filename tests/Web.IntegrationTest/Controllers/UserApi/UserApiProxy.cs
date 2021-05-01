using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;

namespace Web.IntegrationTest.Controllers.UserApi
{
    internal class UserApiProxy
    {
        private readonly HttpClient httpClient;

        public UserApiProxy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateUserCommand createUserCommand)
        {
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(createUserCommand)
            });
        }
    }
}