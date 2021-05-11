using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class UpdateWorkspaceApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        private UpdateWorkspaceRequest DefaultRequest
            => new UpdateWorkspaceRequest
            {
                Id = WorkspaceId,
                Name = WorkspaceName,
                AccessToken = AccessToken,
                GatewayUrl = GatewayUrl
            };

        [Test]
        public async Task Update_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }

        [Test]
        public async Task Update_InvalidModel_ShouldReturnErrorMessage()
        {
            UpdateWorkspaceRequest requestWithoutMandatoryParams = new UpdateWorkspaceRequest();
            OperationResult response = await WorkspaceApiClient.UpdateAsync(requestWithoutMandatoryParams);
            Assert.AreEqual(OperationStatus.Error, response.Status);
        }

        [Test]
        public async Task Update_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultRequest with { Id = nonExistingWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", response.Message);
        }

        [Test]
        public async Task Update_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Forbidden, response.Status);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", response.Message);
        }

        [Test]
        public async Task Update_NewGatewayUrlIsTaken_ShouldReturnErrorMessage()
        {
            string otherGatewayUrl = "http://other.website.com";
            CreateWorkspaceRequest createOtherWorkspaceRequest = StoredWorkspace with { GatewayUrl = otherGatewayUrl };
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(otherGatewayUrl, AccessToken)).ReturnsAsync(true);
            await WorkspaceApiClient.CreateAsync(createOtherWorkspaceRequest);
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultRequest with { GatewayUrl = otherGatewayUrl });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {otherGatewayUrl}", response.Message);
        }

        [Test]
        public async Task Update_CanNotConnectToNewGateway_ShouldReturnErrorMessage()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(false);
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual("Can not connect to gateway using the provided url and access token", response.Message);
        }

        [Test]
        public async Task Update_Success_ShouldUpdateWorkspace()
        {
            UpdateWorkspaceRequest updateWorkspaceRequest = new UpdateWorkspaceRequest
            {
                Id = WorkspaceId,
                Name = "Updated Name",
                GatewayUrl = "http://updated.url.com",
                AccessToken = "updated.jwt.token"
            };

            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(updateWorkspaceRequest.GatewayUrl, updateWorkspaceRequest.AccessToken)).ReturnsAsync(true);
            OperationResult response = await WorkspaceApiClient.UpdateAsync(updateWorkspaceRequest);
            OperationResult<WorkspaceApiModel> updatedWorkspaceResponse = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Success, response.Status);
            Assert.IsTrue(AreEqual(updateWorkspaceRequest, updatedWorkspaceResponse.Data));
        }

        private bool AreEqual(UpdateWorkspaceRequest updateWorkspaceRequest, WorkspaceApiModel workspaceApiModel)
            => updateWorkspaceRequest.Id == workspaceApiModel.Id
               && updateWorkspaceRequest.Name == workspaceApiModel.Name
               && updateWorkspaceRequest.GatewayUrl == workspaceApiModel.GatewayUrl
               && updateWorkspaceRequest.AccessToken == workspaceApiModel.AccessToken;
    }
}