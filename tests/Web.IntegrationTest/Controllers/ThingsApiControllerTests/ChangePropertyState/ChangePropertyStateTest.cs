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
using Web.Models.Things.Request;
using Web.Models.User.Request;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests.ChangePropertyState
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class ChangePropertyStateTest : WebApiIntegrationTestBase
    {
        private const string WorkspaceName = "TestName";
        private const string GatewayUrl = "http://localhost:1214";
        private const string AccessToken = "j.w.t";
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string ThingId = "http://localhost:1214/things/virtual-things-0";
        private const string PropertyName = "on";
        private const string CorrectNewValue = "true";

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
        public async Task ChangePropertyState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            await userApiClient.LogoutAsync();
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task ChangePropertyState_InvalidModel_ShouldReturnErrorMessage()
        {
            var requestWithoutFields = new ChangePropertyStateRequest();
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(requestWithoutFields);
            Assert.AreEqual(OperationStatus.Error, result.Status);
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetThing_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThings();
            MockGatewayThingsEndpoint(serializedThings);
            string nonExistingThingId = "non-existing-id";
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest() with { ThingId = nonExistingThingId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Message about non existing id. Replace with actual message", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetProperty_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThings();
            MockGatewayThingsEndpoint(serializedThings);
            string nonExistingPropertyName = "non-existing-property";
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest() with { PropertyName = nonExistingPropertyName });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Message about non existing property. Replace with actual message", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_InvalidNewValue_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThings();
            MockGatewayThingsEndpoint(serializedThings);
            string invalidNewValue = "invalid";
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest() with { NewPropertyValue = invalidNewValue });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Message about invalid property value. Replace with actual message", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_Success_ShouldReturnSuccessOperationResult()
        {
            string serializedThings = await GetSerializedThings();
            MockGatewayThingsEndpoint(serializedThings);
            MockGatewayUpdatePropertyEndpoint();
            OperationResult result = await thingsApiClient.ChangePropertyStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Success, result.Status);
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            MockHttpClientFactory(services: services);
        }

        private void MockGatewayUpdatePropertyEndpoint()
        {
            string expectedContent = "{ \"on\": true }";

            SetupHttpMessageHandlerMock(
                request =>
                    CheckHttpRequestMessage(request, ThingId + "/properties/" + PropertyName, HttpMethod.Put)
                    && request.Content?.ReadAsStringAsync().Result == expectedContent,
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
        }

        private void MockGatewayThingsEndpoint(string returnedContent)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, GatewayUrl + "/things", HttpMethod.Get),
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

        private ChangePropertyStateRequest CreateRequest()
            => new ChangePropertyStateRequest
            {
                WorkspaceId = workspaceId,
                ThingId = ThingId,
                PropertyName = PropertyName,
                NewPropertyValue = CorrectNewValue
            };

        private async Task<string> GetSerializedThings()
        {
            string resourcesFolder = Path.GetFullPath("Controllers/ThingsApiControllerTests/ChangePropertyState/things.json");
            return await File.ReadAllTextAsync(resourcesFolder);
        }
    }
}