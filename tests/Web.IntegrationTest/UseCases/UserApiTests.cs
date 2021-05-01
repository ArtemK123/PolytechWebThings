using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Web.IntegrationTest.UseCases
{
    internal class UserApiTests
    {
        private WebApplicationFactory<Startup> webApplicationFactory;
        private HttpClient httpClient;

        [SetUp]
        public void SetUp()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            httpClient = webApplicationFactory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
            webApplicationFactory.Dispose();
        }

        [Test]
        public async Task RegisterAndLoginTest()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";

            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password })
            });

            HttpResponseMessage loginResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Login")
            {
                Content = JsonContent.Create(new LoginUserCommand { Email = email, Password = password })
            });

            Assert.True(loginResponse.IsSuccessStatusCode);
            Assert.False(string.IsNullOrEmpty(await loginResponse.Content.ReadAsStringAsync()));
        }
    }
}