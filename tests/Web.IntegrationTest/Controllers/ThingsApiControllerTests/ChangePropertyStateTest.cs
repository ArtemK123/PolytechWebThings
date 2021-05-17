using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Things.Request;
using Web.Models.User.Request;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class ChangePropertyStateTest : WebApiIntegrationTestBase
    {
        private const string WorkspaceName = "TestName";
        private const string GatewayUrl = "http://localhost:1214";
        private const string AccessToken = "j.w.t";
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "another@test.com";

        private UserApiClient userApiClient;
        private WorkspaceApiClient workspaceApiClient;
        private Mock<HttpMessageHandler> httpMessageHandlerMock;
        private CreateWorkspaceRequest storedWorkspace;
        private int workspaceId;

        private ThingsApiClient thingsApiClient;

        [SetUp]
        public async Task SetUp()
        {
            userApiClient = new UserApiClient(HttpClient, new HttpResponseMessageParser());
            workspaceApiClient = new WorkspaceApiClient(HttpClient, new HttpResponseMessageParser());
            await userApiClient.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await userApiClient.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
            storedWorkspace = new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl };
            MockPingGatewayHttpCall();
            await workspaceApiClient.CreateAsync(storedWorkspace);
            OperationResult<GetUserWorkspacesResponse> getUserWorkspacesResponse = await workspaceApiClient.GetUserWorkspacesAsync();
            workspaceId = getUserWorkspacesResponse.Data.Workspaces.First().Id;
            thingsApiClient = new ThingsApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task ChangePropertyState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            await userApiClient.LogoutAsync();
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task ChangePropertyState_InvalidModel_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetThing_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetProperty_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_InvalidNewValue_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_Success_ShouldReturnSuccessOperationResult()
        {
            throw new NotImplementedException();
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MockHttpClientFactory(services: services);
        }

        private async Task ChangeUserAsync()
        {
            await userApiClient.LogoutAsync();
            await userApiClient.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await userApiClient.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
        }

        private void MockPingGatewayHttpCall(HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            SetupHttpMessageHandlerMock(
                request
                    => request.RequestUri?.AbsoluteUri == GatewayUrl + "/ping"
                       && request.Method == HttpMethod.Get
                       && request.Headers.Authorization?.Scheme == "Bearer"
                       && request.Headers.Authorization?.Parameter == AccessToken
                       && request.Headers.Accept.Single().MediaType == "application/json",
                new HttpResponseMessage(responseStatus));
        }

        private void SetupHttpMessageHandlerMock(Func<HttpRequestMessage, bool> isRequestValidFunc, HttpResponseMessage response)
        {
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => isRequestValidFunc(message)), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        private void MockHttpClientFactory(IServiceCollection services)
        {
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            services.AddTransient(_ => httpClientFactoryMock.Object);
        }

        private ChangePropertyStateRequest CreateRequest(string newValue = null)
            => new ChangePropertyStateRequest
            {
                WorkspaceId = workspaceId,
                ThingId = "thingId",
                PropertyName = "propertyName",
                NewPropertyValue = newValue
            };
    }
}