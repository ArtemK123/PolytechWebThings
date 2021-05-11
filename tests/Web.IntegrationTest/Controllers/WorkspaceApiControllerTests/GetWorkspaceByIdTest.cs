using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class GetWorkspaceByIdTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        [Test]
        public async Task GetById_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<WorkspaceApiModel> response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }

        [TestCase(null, TestName = "Should return bad request when workspaceId is missing")]
        [TestCase(-1, TestName = "Should return bad request when workspaceId is invalid")]
        public async Task GetById_InvalidModel_ShouldReturnErrorMessage(int? invalidId)
        {
            OperationResult<WorkspaceApiModel> response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = invalidId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
        }

        [Test]
        public async Task GetById_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            OperationResult<WorkspaceApiModel> response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = nonExistingWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", response.Message);
        }

        [Test]
        public async Task GetById_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            OperationResult<WorkspaceApiModel> response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Forbidden, response.Status);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", response.Message);
        }

        [Test]
        public async Task GetById_Success_ShouldReturnErrorMessage()
        {
            OperationResult<WorkspaceApiModel> response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { Id = WorkspaceId });
            Assert.AreEqual(OperationStatus.Success, response.Status);
            Assert.IsTrue(AreSame(StoredWorkspace, WorkspaceId, response.Data));
        }

        private bool AreSame(CreateWorkspaceRequest request, int id, WorkspaceApiModel apiModel)
            => request.Name == apiModel.Name
               && request.AccessToken == apiModel.AccessToken
               && request.GatewayUrl == apiModel.GatewayUrl
               && id == apiModel.Id;
    }
}