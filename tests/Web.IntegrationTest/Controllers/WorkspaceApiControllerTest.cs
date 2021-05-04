using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.UserApiController;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.IntegrationTest.Controllers
{
    [TestFixture]
    internal class WorkspaceApiControllerTest : WebApiIntegrationTestBase
    {
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

            string expectedResponseMessage =
                "{\"Name\":[\"'Name' must not be empty.\"],\"GatewayUrl\":[\"'Gateway Url' must not be empty.\"],\"AccessToken\":[\"'Access Token' must not be empty.\"]}";
            string actualResponseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedResponseMessage, actualResponseMessage);
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
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {GatewayUrl}", responseMessage);
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
            Assert.AreEqual("Can not connect to gateway using the provided url and access token", responseMessage);
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

        [Test]
        public async Task GetUserWorkspaces_Unauthorized_ShouldReturnUnauthorizedResponse()
        {
            await userApiProxy.LogoutAsync();
            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetUserWorkspaces_NoWorkspaces_ShouldReturnEmptyCollection()
        {
            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            GetUserWorkspacesResponse responseData = JsonSerializer.Deserialize<GetUserWorkspacesResponse>(await response.Content.ReadAsStringAsync()) ?? throw new NullReferenceException();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(responseData.Workspaces);
        }

        [Test]
        public async Task GetUserWorkspaces_WithWorkspaces_ShouldReturnAllWorkspaces()
        {
            IReadOnlyCollection<CreateWorkspaceRequest> createWorkspaceRequests = new[]
            {
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}1", AccessToken = $"{AccessToken}1", GatewayUrl = $"{GatewayUrl}1" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}2", AccessToken = $"{AccessToken}2", GatewayUrl = $"{GatewayUrl}2" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}3", AccessToken = $"{AccessToken}3", GatewayUrl = $"{GatewayUrl}3" },
            };

            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            GetUserWorkspacesResponse responseData = JsonSerializer.Deserialize<GetUserWorkspacesResponse>(await response.Content.ReadAsStringAsync()) ?? throw new NullReferenceException();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(
                responseData
                    .Workspaces.All(responseModel =>
                        createWorkspaceRequests.Any(
                            requestModel => AreSame(requestModel, responseModel))));
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            services.AddTransient(_ => gatewayConnectorMock.Object);
        }

        private async Task<HttpResponseMessage> SendCreateWorkspaceRequest(CreateWorkspaceRequest requestModel)
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/WorkspaceApi/Create")
            {
                Content = JsonContent.Create(requestModel)
            });
        }

        private async Task<HttpResponseMessage> SendGetUserWorkspacesRequest()
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/WorkspaceApi/GetUserWorkspaces"));
        }

        private bool AreSame(CreateWorkspaceRequest requestModel, WorkspaceApiModel responseModel)
            => requestModel.Name == responseModel.Name
               && requestModel.GatewayUrl == responseModel.GatewayUrl;
    }
}