using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.CommonTestBases;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class UpdateWorkspaceApiTest : StoredWorkspaceApiTestBase
    {
        private const string OtherGatewayUrl = "http://other.website.com";

        private UpdateWorkspaceRequest DefaultUpdateWorkspaceRequest
            => new UpdateWorkspaceRequest
            {
                Id = WorkspaceId,
                Name = WorkspaceName,
                AccessToken = AccessToken,
                GatewayUrl = GatewayUrl
            };

        private CreateWorkspaceRequest DefaultCreateWorkspaceRequest => new CreateWorkspaceRequest { Name = WorkspaceName, GatewayUrl = GatewayUrl, AccessToken = AccessToken };

        [Test]
        public async Task Update_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultUpdateWorkspaceRequest);
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
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultUpdateWorkspaceRequest with { Id = nonExistingWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", response.Message);
        }

        [Test]
        public async Task Update_UserHasNotEnoughRights_ShouldReturnErrorMessage()
        {
            await ChangeUserAsync();
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultUpdateWorkspaceRequest);
            Assert.AreEqual(OperationStatus.Forbidden, response.Status);
            Assert.AreEqual($"User does not have rights to perform this action - Get workspace with id={WorkspaceId}", response.Message);
        }

        [Test]
        public async Task Update_NewGatewayUrlIsTaken_ShouldReturnErrorMessage()
        {
            CreateWorkspaceRequest createOtherWorkspaceRequest = DefaultCreateWorkspaceRequest with { GatewayUrl = OtherGatewayUrl };
            MockGatewayPingEndpoint(OtherGatewayUrl, AccessToken, HttpStatusCode.OK);
            await WorkspaceApiClient.CreateAsync(createOtherWorkspaceRequest);
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultUpdateWorkspaceRequest with { GatewayUrl = OtherGatewayUrl });
            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {OtherGatewayUrl}", response.Message);
        }

        [Test]
        public async Task Update_CanNotConnectToNewGateway_ShouldReturnErrorMessage()
        {
            MockGatewayPingEndpoint(OtherGatewayUrl, AccessToken, HttpStatusCode.Unauthorized);
            OperationResult response = await WorkspaceApiClient.UpdateAsync(DefaultUpdateWorkspaceRequest with { GatewayUrl = OtherGatewayUrl });
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
                GatewayUrl = OtherGatewayUrl,
                AccessToken = "updated.jwt.token"
            };

            MockGatewayPingEndpoint(gatewayUrl: updateWorkspaceRequest.GatewayUrl, accessToken: updateWorkspaceRequest.AccessToken, responseStatus: HttpStatusCode.OK);
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

        private void MockGatewayPingEndpoint(string gatewayUrl, string accessToken, HttpStatusCode responseStatus)
        {
            SetupHttpMessageHandlerMock(
                request => IsValidPingRequest(request, gatewayUrl, accessToken),
                new HttpResponseMessage(responseStatus));
        }

        private bool IsValidPingRequest(HttpRequestMessage request, string gatewayUrl, string accessToken)
            => request.RequestUri?.AbsoluteUri == gatewayUrl + "/ping"
               && request.Method == HttpMethod.Get
               && request.Headers.Authorization?.Scheme == "Bearer"
               && request.Headers.Authorization?.Parameter == accessToken
               && request.Headers.Accept.Single().MediaType == "application/json";
    }
}