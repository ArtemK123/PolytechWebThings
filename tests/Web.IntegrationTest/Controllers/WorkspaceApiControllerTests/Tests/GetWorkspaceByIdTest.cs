using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
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
    internal class GetWorkspaceByIdTest : WorkspaceApiControllerTestBase
    {
        private const string AnotherUserEmail = "another@test.com";
        private CreateWorkspaceRequest createWorkspaceRequest;
        private int workspaceId;

        [SetUp]
        public async Task SetUp()
        {
            createWorkspaceRequest = new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl };
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            await WorkspaceApiClient.CreateAsync(createWorkspaceRequest);
            GetUserWorkspacesResponse getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesParsedResponseAsync();
            workspaceId = getUserWorkspacesResponse.Workspaces.First().Id;
        }

        [Test]
        public async Task GetById_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { WorkspaceId = workspaceId });
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetById_InvalidModel_ShouldReturnErrorMessage()
        {
            int invalidId = -1;
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { WorkspaceId = invalidId });
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task GetById_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = workspaceId + 1;
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { WorkspaceId = nonExistingWorkspaceId });
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task GetById_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await UserApiProxy.LogoutAsync();
            await UserApiProxy.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await UserApiProxy.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { WorkspaceId = workspaceId });
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Test]
        public async Task GetById_Success_ShouldReturnErrorMessage()
        {
            HttpResponseMessage response = await WorkspaceApiClient.GetByIdAsync(new GetWorkspaceByIdRequest { WorkspaceId = workspaceId });
            string responseText = await response.Content.ReadAsStringAsync();
            WorkspaceApiModel workspaceApiModel = JsonSerializer.Deserialize<WorkspaceApiModel>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            Assert.IsTrue(AreSame(createWorkspaceRequest, workspaceId, workspaceApiModel));
        }

        private bool AreSame(CreateWorkspaceRequest request, int id, WorkspaceApiModel apiModel)
            => request.Name == apiModel.Name
               && request.AccessToken == apiModel.AccessToken
               && request.GatewayUrl == apiModel.GatewayUrl
               && id == apiModel.Id;
    }
}