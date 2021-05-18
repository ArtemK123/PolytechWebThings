using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Things.Request;
using Web.Models.Things.Response;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests.GetWorkspaceWithThings
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class GetWorkspaceWithThingsApiTest : ThingsApiControllerTestBase
    {
        [Test]
        public async Task GetWorkspaceWithThings_PrimitivePropertyValues_ShouldDeserializeAndReturnThings()
        {
            string serializedThings = await ReadContentFromDiskAsync("Controllers/ThingsApiControllerTests/GetWorkspaceWithThings/PrimitivePropertyValues/Input.json");
            string serializedExpected = await ReadContentFromDiskAsync("Controllers/ThingsApiControllerTests/GetWorkspaceWithThings/PrimitivePropertyValues/Expected.json");

            MockGatewayThingsEndpoint(serializedThings);

            OperationResult<GetWorkspaceWithThingsResponse> result = await ThingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            string serializedActual = JsonSerializer.Serialize(result.Data);

            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreEqual(serializedExpected, serializedActual);
        }

        [Test]
        public async Task GetWorkspaceWithThings_NoResponseFromGateway_ShouldInformUserAboutProblem()
        {
            HttpRequestException thrownException = new HttpRequestException("Some problem with http connection");

            SetupHttpMessageHandlerMockWithException(IsGatewayThingsEndpointRequest, thrownException);

            OperationResult<GetWorkspaceWithThingsResponse> result = await ThingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });

            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not connect to {WorkspaceName} gateway at {GatewayUrl}", result.Message);
        }

        [Test]
        public async Task GetWorkspaceWithThings_UnauthorizedResponseFromGateway_ShouldInformUserAboutProblem()
        {
            MockGatewayThingsEndpoint(HttpStatusCode.Unauthorized);

            OperationResult<GetWorkspaceWithThingsResponse> result = await ThingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Invalid access token for {WorkspaceName} gateway at {GatewayUrl}. Please, update access token and try again", result.Message);
        }

        [Test]
        public async Task GetWorkspaceWithThings_InvalidPayloadFromGateway_ShouldInformUserAboutProblem()
        {
            MockGatewayThingsEndpoint("<doctype html><html><head></head><body></body></html>");

            OperationResult<GetWorkspaceWithThingsResponse> result = await ThingsApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"{WorkspaceName} gateway returned invalid data, which can not be parsed. If this error remains, please contact the support", result.Message);
        }

        [Test]
        public async Task GetWorkspaceWithThings_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            GetWorkspaceWithThingsRequest invalidRequest = new GetWorkspaceWithThingsRequest { WorkspaceId = -1 };
            OperationResult<GetWorkspaceWithThingsResponse> result = await ThingsApiClient.GetWorkspaceWithThingsAsync(invalidRequest);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("{\"WorkspaceId\":[\"Non-positive ids are not supported\"]}", result.Message);
        }
    }
}