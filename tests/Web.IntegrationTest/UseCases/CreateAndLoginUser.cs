using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Web.IntegrationTest.UseCases
{
    internal class CreateAndLoginUser
    {
        private WebApplicationFactory<Startup> webApplicationFactory;
        private HttpClient httpClient;

        [SetUp]
        public void SetUp()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
            webApplicationFactory.Dispose();
        }

        [Test]
        public async Task RegisterTest()
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "http://localhost:7406/api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = "test321@gmail.com", Password = "12345678" })
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}