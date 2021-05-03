using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.UserApiController;
using Web.Models.Request;

namespace Web.IntegrationTest.Controllers
{
    [TestFixture]
    internal class WorkspaceApiControllerTest : WebApiIntegrationTestBase
    {
        private const string CreateActionUrl = "api/WorkspaceApi/Create";
        private const string UserEmail = "test@gmail.com";
        private const string UserPassword = "123123";
        private const string WorkspaceName = "TestName";
        private const string GatewayUrl = "http://localhost:8080";
        private const string AccessToken = "j.w.t";

        private UserApiProxy userApiProxy;
        private Mock<IGatewayConnector> gatewayConnectorMock;

        [SetUp]
        public async Task SetUp()
        {
            gatewayConnectorMock = new Mock<IGatewayConnector>(MockBehavior.Strict);
            userApiProxy = new UserApiProxy(HttpClient);
            await userApiProxy.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await userApiProxy.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
        }

        [Test]
        public async Task Create_InvalidModel_ShouldReturnBadRequestResponse()
        {
            HttpResponseMessage response = await SendCreateWorkspaceRequest(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            string responseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("text to replace with actual message", responseMessage);
        }

        [Test]
        public async Task Create_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await userApiProxy.LogoutAsync();

            HttpResponseMessage response = await SendCreateWorkspaceRequest(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Create_GatewayUrlIsAlreadyUsed_ShouldReturnBadRequestResponse()
        {
            gatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            var firstRequest = new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            };
            var secondRequest = firstRequest with { Name = "other name", AccessToken = "otherj.w.t" };

            await SendCreateWorkspaceRequest(firstRequest);
            HttpResponseMessage response = await SendCreateWorkspaceRequest(secondRequest);

            string responseMessage = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("text to replace with actual message", responseMessage);
        }

        [Test]
        public async Task Create_CannotConnectToGateway_ShouldReturnBadRequestResponse()
        {
            gatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(false);
            HttpResponseMessage response = await SendCreateWorkspaceRequest(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            string responseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("text to replace with actual message", responseMessage);
        }

        [Test]
        public async Task Create_Success_ShouldReturnOkResponse()
        {
            gatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            HttpResponseMessage response = await SendCreateWorkspaceRequest(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            services.AddTransient(_ => gatewayConnectorMock.Object);
        }

        private async Task<HttpResponseMessage> SendCreateWorkspaceRequest(CreateWorkspaceRequest requestModel)
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, CreateActionUrl)
            {
                Content = JsonContent.Create(requestModel)
            });
        }
    }
}