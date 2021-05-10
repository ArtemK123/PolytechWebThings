using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.User.Request;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class DeleteWorkspaceApiTest : WorkspaceApiControllerTestBase
    {
        private const string AnotherUserEmail = "another@test.com";

        private int workspaceId;

        [SetUp]
        public async Task SetUp()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            await WorkspaceApiClient.CreateWorkspaceAsync(new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl });
            GetUserWorkspacesResponse getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesParsedResponseAsync();
            workspaceId = getUserWorkspacesResponse.Workspaces.First().Id;
        }

        [Test]
        public async Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await WorkspaceApiClient.DeleteWorkspaceAsync(new DeleteWorkspaceRequest());
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Delete_UserDoesNotHaveEnoughRights_ShouldReturnErrorMessage()
        {
            await UserApiProxy.LogoutAsync();
            await UserApiProxy.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await UserApiProxy.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            HttpResponseMessage response = await WorkspaceApiClient.DeleteWorkspaceAsync(new DeleteWorkspaceRequest { WorkspaceId = workspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("User has not enough rights to delete this workspace", responseText);
        }

        [Test]
        public async Task Delete_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = workspaceId + 1;
            HttpResponseMessage response = await WorkspaceApiClient.DeleteWorkspaceAsync(new DeleteWorkspaceRequest { WorkspaceId = nonExistingWorkspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Workspace is not found", responseText);
        }

        [Test]
        public async Task Delete_Success_ShouldReturnOkResponse()
        {
            HttpResponseMessage deleteWorkspaceResponse = await WorkspaceApiClient.DeleteWorkspaceAsync(new DeleteWorkspaceRequest { WorkspaceId = workspaceId });
            GetUserWorkspacesResponse getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesParsedResponseAsync();

            Assert.AreEqual(HttpStatusCode.OK, deleteWorkspaceResponse.StatusCode);
            Assert.IsEmpty(getUserWorkspacesResponse.Workspaces);
        }
    }
}