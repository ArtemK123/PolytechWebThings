using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class DeleteWorkspaceApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        [Test]
        public async Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestCase(null, "{\"Id\":[\"'Id' must not be empty.\"]}", TestName = "Should return bad request when workspaceId is missing")]
        [TestCase(0, "{\"Id\":[\"Non-positive ids are not supported\"]}", TestName = "Should return bad request when workspaceId is invalid")]
        public async Task Delete_InvalidRequestModel_ShouldReturnBadRequest(int? invalidWorkspaceId, string expectedValidationMessage)
        {
            HttpResponseMessage response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = invalidWorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedValidationMessage, responseText);
        }

        [Test]
        public async Task Delete_UserDoesNotHaveEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            HttpResponseMessage response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", responseText);
        }

        [Test]
        public async Task Delete_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            HttpResponseMessage response = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = nonExistingWorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", responseText);
        }

        [Test]
        public async Task Delete_Success_ShouldReturnOkResponse()
        {
            HttpResponseMessage deleteWorkspaceResponse = await WorkspaceApiClient.DeleteAsync(new DeleteWorkspaceRequest { Id = WorkspaceId });
            GetUserWorkspacesResponse getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesParsedResponseAsync();

            Assert.AreEqual(HttpStatusCode.OK, deleteWorkspaceResponse.StatusCode);
            Assert.IsEmpty(getUserWorkspacesResponse.Workspaces);
        }
    }
}