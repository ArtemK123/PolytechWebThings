using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class CreateWorkspaceApiTest : WorkspaceApiControllerTestBase
    {
        [Test]
        public async Task Create_InvalidModel_ShouldReturnBadRequestResponse()
        {
            OperationResult response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            string expectedResponseMessage =
                "{\"Name\":[\"'Name' must not be empty.\"],\"GatewayUrl\":[\"'Gateway Url' must not be empty.\"],\"AccessToken\":[\"'Access Token' must not be empty.\"]}";

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual(expectedResponseMessage, response.Message);
        }

        [Test]
        public async Task Create_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiClient.LogoutAsync();

            OperationResult response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }

        [Test]
        public async Task Create_GatewayUrlIsAlreadyUsed_ShouldReturnBadRequestResponse()
        {
            MockGatewayConnectionCheck(validConnection: true);
            var firstRequest = new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            };
            var secondRequest = firstRequest with { Name = "other name", AccessToken = "otherj.w.t" };

            await WorkspaceApiClient.CreateAsync(firstRequest);
            OperationResult response = await WorkspaceApiClient.CreateAsync(secondRequest);

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {GatewayUrl}", response.Message);
        }

        [Test]
        public async Task Create_CannotConnectToGateway_ShouldReturnBadRequestResponse()
        {
            MockGatewayConnectionCheck(validConnection: false);
            OperationResult response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual("Can not connect to gateway using the provided url and access token", response.Message);
        }

        [Test]
        public async Task Create_Success_ShouldReturnOkResponse()
        {
            MockGatewayConnectionCheck(validConnection: true);
            OperationResult response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            Assert.AreEqual(OperationStatus.Success, response.Status);
        }
    }
}