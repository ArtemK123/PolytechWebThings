using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.WorkspaceApiControllerTests;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTest.GetWorkspaceWithThings
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class GetWorkspaceWithThingsApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        private ThingsApiClient thingsApiClient;

        [SetUp]
        public void SetUp()
        {
            thingsApiClient = new ThingsApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task GetWorkspaceWithThings_PrimitivePropertyValues_ShouldDeserializeAndReturnThings()
        {
            string resourcesFolder = Path.GetFullPath("Controllers/WorkspaceApiControllerTests/GetWorkspaceWithThings/PrimitivePropertyValues");
            await SetupHttpMessageHandlerMockWithValidResponse(resourcesFolder);
            string serializedExpected = await File.ReadAllTextAsync(Path.Combine(resourcesFolder, "Expected.json"));

            OperationResult<GetWorkspaceWithThingsResponse> result = await thingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            string serializedActual = JsonSerializer.Serialize(result.Data);

            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreEqual(serializedExpected, serializedActual);
        }

        [Test]
        public async Task GetWorkspaceWithThings_NoResponseFromGateway_ShouldInformUserAboutProblem()
        {
            HttpRequestException thrownException = new HttpRequestException("Some problem with http connection");

            HttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => IsExpectedHttpResponseMessage(message)), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(thrownException);

            OperationResult<GetWorkspaceWithThingsResponse> result = await thingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });

            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not connect to {WorkspaceName} gateway at {GatewayUrl}", result.Message);
        }

        [Test]
        public async Task GetWorkspaceWithThings_UnauthorizedResponseFromGateway_ShouldInformUserAboutProblem()
        {
            SetupHttpMessageHandlerMock(IsExpectedHttpResponseMessage, new HttpResponseMessage(HttpStatusCode.Unauthorized));

            OperationResult<GetWorkspaceWithThingsResponse> result = await thingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Invalid access token for {WorkspaceName} gateway at {GatewayUrl}. Please, update access token and try again", result.Message);
        }

        [Test]
        public async Task GetWorkspaceWithThings_InvalidPayloadFromGateway_ShouldInformUserAboutProblem()
        {
            string returnedPayload = "<doctype html><html><head></head><body></body></html>";
            SetupHttpMessageHandlerMock(IsExpectedHttpResponseMessage, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(returnedPayload) });

            OperationResult<GetWorkspaceWithThingsResponse> result = await thingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"{WorkspaceName} gateway returned invalid data, which can not be parsed. If this error remains, please contact the support", result.Message);
        }

        private async Task SetupHttpMessageHandlerMockWithValidResponse(string resourcesFolder)
        {
            string inputPath = Path.Combine(resourcesFolder, "Input.json");
            string jsonFileContent = await File.ReadAllTextAsync(inputPath);
            SetupHttpMessageHandlerMock(
                IsExpectedHttpResponseMessage,
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(jsonFileContent) });
        }

        private bool IsExpectedHttpResponseMessage(HttpRequestMessage httpRequestMessage)
            => httpRequestMessage.RequestUri?.AbsoluteUri == GatewayUrl + "/things"
               && httpRequestMessage.Method == HttpMethod.Get
               && httpRequestMessage.Headers.Authorization?.Scheme == "Bearer"
               && httpRequestMessage.Headers.Authorization?.Parameter == AccessToken
               && httpRequestMessage.Headers.Accept.Single().MediaType == "application/json";
    }
}