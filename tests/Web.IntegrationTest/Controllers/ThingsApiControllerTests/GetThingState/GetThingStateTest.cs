using System;
using System.IO;
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
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Models.User.Request;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests.GetThingState
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class GetThingStateTest : WebApiIntegrationTestBase
    {
        private const string WorkspaceName = "TestName";
        private const string GatewayUrl = "http://localhost:1214";
        private const string AccessToken = "j.w.t";
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string ThingId = "http://localhost:1214/things/virtual-things-0";
        private const string ThingsInputPath = "Controllers/ThingsApiControllerTests/GetThingState/thing.json";

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
            MockGatewayPingEndpoint();
            await workspaceApiClient.CreateAsync(storedWorkspace);
            OperationResult<GetUserWorkspacesResponse> getUserWorkspacesResponse = await workspaceApiClient.GetUserWorkspacesAsync();
            workspaceId = getUserWorkspacesResponse.Data.Workspaces.First().Id;
            thingsApiClient = new ThingsApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task GetThingState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            await userApiClient.LogoutAsync();
            OperationResult result = await thingsApiClient.GetThingStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task GetThingState_InvalidModel_ShouldReturnErrorMessage()
        {
            string expectedMessage = "{\"ThingId\":[\"'Thing Id' must not be empty.\"],\"WorkspaceId\":[\"'Workspace Id' must not be empty.\"]}";
            GetThingStateRequest emptyRequestModel = new GetThingStateRequest();
            OperationResult result = await thingsApiClient.GetThingStateAsync(emptyRequestModel);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public async Task GetThingState_ThingIsNotFound_ShouldReturnErrorMessage()
        {
            string nonExistingThing = "non-existing-thing-id";
            string thingInput = await ReadInputAsync(ThingsInputPath);
            MockGatewayThingsEndpoint(thingInput);
            OperationResult result = await thingsApiClient.GetThingStateAsync(CreateRequest() with { ThingId = nonExistingThing });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not find thing with id={nonExistingThing}", result.Message);
        }

        [Test]
        public async Task GetThingState_Success_ShouldReturnThingState()
        {
            string thingInput = await ReadInputAsync(ThingsInputPath);
            string propertyInput = "{ \"color\": \"#ff00aa\", \"colorTemperature\": 4000, \"colorMode\": \"temperature\", \"level\": 70, \"on\": true }";
            MockGatewayThingsEndpoint(thingInput);
            MockGatewayThingPropertiesEndpoint(propertyInput);
            OperationResult<ThingStateApiModel> result = await thingsApiClient.GetThingStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreEqual(result.Data.ThingId, ThingId);
            Assert.AreEqual(result.Data.PropertyStates["color"], "#ff00aa");
            Assert.AreEqual(result.Data.PropertyStates["colorTemperature"], "4000");
            Assert.AreEqual(result.Data.PropertyStates["colorMode"], "temperature");
            Assert.AreEqual(result.Data.PropertyStates["level"], "70");
            Assert.AreEqual(result.Data.PropertyStates["on"], "true");
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MockHttpClientFactory(services: services);
        }

        private void MockGatewayThingsEndpoint(string returnedContent)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, GatewayUrl + "/things", HttpMethod.Get),
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(returnedContent) });
        }

        private void MockGatewayThingPropertiesEndpoint(string returnedContent)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, ThingId + "/properties", HttpMethod.Get),
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(returnedContent) });
        }

        private void MockGatewayPingEndpoint(HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, GatewayUrl + "/ping", HttpMethod.Get),
                new HttpResponseMessage(responseStatus));
        }

        private bool CheckHttpRequestMessage(HttpRequestMessage request, string url, HttpMethod method)
            => request.RequestUri?.AbsoluteUri == url
               && request.Method == method
               && request.Headers.Authorization?.Scheme == "Bearer"
               && request.Headers.Authorization?.Parameter == AccessToken
               && request.Headers.Accept.Single().MediaType == "application/json";

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

        private async Task<string> ReadInputAsync(string pathFromProjRoot)
        {
            string resourcesFolder = Path.GetFullPath(pathFromProjRoot);
            return await File.ReadAllTextAsync(resourcesFolder);
        }

        private GetThingStateRequest CreateRequest()
            => new GetThingStateRequest { ThingId = ThingId, WorkspaceId = workspaceId };
    }
}