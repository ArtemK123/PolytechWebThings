using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Things.Request;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests.ChangePropertyState
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class ChangePropertyStateTest : ThingsApiControllerTestBase
    {
        private const string ThingId = "http://localhost:1214/things/virtual-things-0";
        private const string ThingTitle = "Virtual On/Off Color Light";
        private const string PropertyName = "on";
        private const string CorrectNewValue = "true";

        [Test]
        public async Task ChangePropertyState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task ChangePropertyState_InvalidModel_ShouldReturnErrorMessage()
        {
            string expectedMessage =
                "{\"ThingId\":[\"'Thing Id' must not be empty.\"],\"WorkspaceId\":[\"'Workspace Id' must not be empty.\"],\"PropertyName\":[\"'Property Name' must not be empty.\"]}";
            var requestWithoutFields = new ChangePropertyStateRequest();
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(requestWithoutFields);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetThing_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThingsAsync();
            MockGatewayThingsEndpoint(serializedThings);
            string nonExistingThingId = "non-existing-id";
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(CreateRequest() with { ThingId = nonExistingThingId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not find thing with id={nonExistingThingId}", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetProperty_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThingsAsync();
            MockGatewayThingsEndpoint(serializedThings);
            string nonExistingPropertyName = "non-existing-property";
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(CreateRequest() with { PropertyName = nonExistingPropertyName });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Can not find property with name={nonExistingPropertyName} in thing {ThingTitle}", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_InvalidNewValue_ShouldReturnErrorMessage()
        {
            string serializedThings = await GetSerializedThingsAsync();
            MockGatewayThingsEndpoint(serializedThings);
            string invalidNewValue = "invalid";
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(CreateRequest() with { NewPropertyValue = invalidNewValue });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Invalid value {invalidNewValue} for property with type {GatewayValueType.Boolean}", result.Message);
        }

        [Test]
        public async Task ChangePropertyState_Success_ShouldReturnSuccessOperationResult()
        {
            string serializedThings = await GetSerializedThingsAsync();
            MockGatewayThingsEndpoint(serializedThings);
            MockGatewayUpdatePropertyEndpoint();
            OperationResult result = await ThingsApiClient.ChangePropertyStateAsync(CreateRequest());
            Assert.AreEqual(OperationStatus.Success, result.Status);
        }

        private void MockGatewayUpdatePropertyEndpoint()
        {
            string expectedContent = "{\"on\":true}";

            SetupHttpMessageHandlerMock(
                request =>
                    CheckHttpRequestMessage(request, ThingId + "/properties/" + PropertyName, HttpMethod.Put)
                    && request.Content?.ReadAsStringAsync().Result == expectedContent,
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
        }

        private ChangePropertyStateRequest CreateRequest()
            => new ChangePropertyStateRequest
            {
                WorkspaceId = WorkspaceId,
                ThingId = ThingId,
                PropertyName = PropertyName,
                NewPropertyValue = CorrectNewValue
            };

        private async Task<string> GetSerializedThingsAsync()
            => await ReadContentFromDiskAsync("Controllers/ThingsApiControllerTests/ChangePropertyState/things.json");
    }
}