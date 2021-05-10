using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class GetWorkspaceByIdTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        [Test]
        public async Task GetById_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdHttpResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestCase(null, TestName = "Should return bad request when workspaceId is missing")]
        [TestCase(-1, TestName = "Should return bad request when workspaceId is invalid")]
        public async Task GetById_InvalidModel_ShouldReturnErrorMessage(int? invalidId)
        {
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdHttpResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = invalidId });
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task GetById_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdHttpResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = nonExistingWorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", responseText);
        }

        [Test]
        public async Task GetById_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdHttpResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = WorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", responseText);
        }

        [Test]
        public async Task GetById_Success_ShouldReturnErrorMessage()
        {
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdHttpResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = WorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            WorkspaceApiModel workspaceApiModel = JsonSerializer.Deserialize<WorkspaceApiModel>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(AreSame(StoredWorkspace, WorkspaceId, workspaceApiModel));
        }

        private bool AreSame(CreateWorkspaceRequest request, int id, WorkspaceApiModel apiModel)
            => request.Name == apiModel.Name
               && request.AccessToken == apiModel.AccessToken
               && request.GatewayUrl == apiModel.GatewayUrl
               && id == apiModel.Id;
    }
}