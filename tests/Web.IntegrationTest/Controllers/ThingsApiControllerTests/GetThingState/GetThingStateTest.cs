using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests.GetThingState
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class GetThingStateTest : ThingsApiControllerTestBase
    {
        private const string ThingId = "http://localhost:1214/things/virtual-things-0";
        private const string ThingsInputPath = "Controllers/ThingsApiControllerTests/GetThingState/thing.json";

        [Test]
        public async Task GetThingState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await ThingsApiClient.GetThingStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task GetThingState_InvalidModel_ShouldReturnErrorMessage()
        {
            string expectedMessage = "{\"ThingId\":[\"'Thing Id' must not be empty.\"],\"WorkspaceId\":[\"'Workspace Id' must not be empty.\"]}";
            GetThingStateRequest emptyRequestModel = new GetThingStateRequest();
            OperationResult result = await ThingsApiClient.GetThingStateAsync(emptyRequestModel);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public async Task GetThingState_ThingIsNotFound_ShouldReturnErrorMessage()
        {
            string nonExistingThing = "non-existing-thing-id";
            string thingInput = await ReadContentFromDiskAsync(ThingsInputPath);
            MockGatewayThingsEndpoint(thingInput);
            OperationResult result = await ThingsApiClient.GetThingStateAsync(CreateRequest() with { ThingId = nonExistingThing });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not find thing with id={nonExistingThing}", result.Message);
        }

        [Test]
        public async Task GetThingState_Success_ShouldReturnThingState()
        {
            string thingInput = await ReadContentFromDiskAsync(ThingsInputPath);
            string propertyInput = "{ \"color\": \"#ff00aa\", \"colorTemperature\": 4000, \"colorMode\": \"temperature\", \"level\": 70, \"on\": true }";
            MockGatewayThingsEndpoint(thingInput);
            MockGatewayThingPropertiesEndpoint(propertyInput);
            OperationResult<ThingStateApiModel> result = await ThingsApiClient.GetThingStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreEqual(result.Data.ThingId, ThingId);
            Assert.AreEqual(result.Data.PropertyStates["color"], "#ff00aa");
            Assert.AreEqual(result.Data.PropertyStates["colorTemperature"], "4000");
            Assert.AreEqual(result.Data.PropertyStates["colorMode"], "temperature");
            Assert.AreEqual(result.Data.PropertyStates["level"], "70");
            Assert.AreEqual(result.Data.PropertyStates["on"], "true");
        }

        private void MockGatewayThingPropertiesEndpoint(string returnedContent)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, ThingId + "/properties", HttpMethod.Get),
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(returnedContent) });
        }

        private GetThingStateRequest CreateRequest()
            => new GetThingStateRequest { ThingId = ThingId, WorkspaceId = WorkspaceId };
    }
}