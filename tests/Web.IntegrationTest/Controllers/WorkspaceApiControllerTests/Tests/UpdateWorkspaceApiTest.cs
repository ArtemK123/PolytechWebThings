using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
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
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Update_InvalidModel_ShouldReturnErrorMessage()
        {
            UpdateWorkspaceRequest requestWithoutMandatoryParams = new UpdateWorkspaceRequest();
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(requestWithoutMandatoryParams);
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Update_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(DefaultRequest with { Id = nonExistingWorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", responseText);
        }

        [Test]
        public async Task Update_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", responseText);
        }

        [Test]
        public async Task Update_NewGatewayUrlIsTaken_ShouldReturnErrorMessage()
        {
            string otherGatewayUrl = "http://other.website.com";
            CreateWorkspaceRequest createOtherWorkspaceRequest = StoredWorkspace with { GatewayUrl = otherGatewayUrl };
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(otherGatewayUrl, AccessToken)).ReturnsAsync(true);
            await WorkspaceApiClient.CreateAsync(createOtherWorkspaceRequest);
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(DefaultRequest with { GatewayUrl = otherGatewayUrl });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {otherGatewayUrl}", responseText);
        }

        [Test]
        public async Task Update_CanNotConnectToNewGateway_ShouldReturnErrorMessage()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(false);
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(DefaultRequest);
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Can not connect to gateway using the provided url and access token", responseText);
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
            HttpResponseMessage response = await WorkspaceApiClient.UpdateAsync(updateWorkspaceRequest);
            WorkspaceApiModel updatedWorkspace = await WorkspaceApiClient.GetByIdParsedResponseAsync(new GetWorkspaceByIdRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(AreEqual(updateWorkspaceRequest, updatedWorkspace));
        }

        private bool AreEqual(UpdateWorkspaceRequest updateWorkspaceRequest, WorkspaceApiModel workspaceApiModel)
            => updateWorkspaceRequest.Id == workspaceApiModel.Id
               && updateWorkspaceRequest.Name == workspaceApiModel.Name
               && updateWorkspaceRequest.GatewayUrl == workspaceApiModel.GatewayUrl
               && updateWorkspaceRequest.AccessToken == workspaceApiModel.AccessToken;
    }
}