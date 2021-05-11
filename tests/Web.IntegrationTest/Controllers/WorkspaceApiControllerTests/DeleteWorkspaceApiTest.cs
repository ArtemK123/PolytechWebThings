using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class DeleteWorkspaceApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        [Test]
        public async Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiClient.LogoutAsync();
            OperationResult response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }

        [TestCase(null, "{\"Id\":[\"'Id' must not be empty.\"]}", TestName = "Should return bad request when workspaceId is missing")]
        [TestCase(0, "{\"Id\":[\"Non-positive ids are not supported\"]}", TestName = "Should return bad request when workspaceId is invalid")]
        public async Task Delete_InvalidRequestModel_ShouldReturnBadRequest(int? invalidWorkspaceId, string expectedValidationMessage)
        {
            OperationResult response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = invalidWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual(expectedValidationMessage, response.Message);
        }

        [Test]
        public async Task Delete_UserDoesNotHaveEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            OperationResult response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Forbidden, response.Status);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", response.Message);
        }

        [Test]
        public async Task Delete_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            OperationResult response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = nonExistingWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", response.Message);
        }

        [Test]
        public async Task Delete_Success_ShouldReturnOkResponse()
        {
            OperationResult deleteWorkspaceResponse = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            OperationResult<GetUserWorkspacesResponse> getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesAsync();

            Assert.AreEqual(OperationStatus.Success, deleteWorkspaceResponse.Status);
            Assert.IsEmpty(getUserWorkspacesResponse.Data.Workspaces);
        }
    }
}