using System.Collections.Generic;
using System.Linq;
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
    internal class GetUserWorkspacesApiTest : WorkspaceApiControllerTestBase
    {
        [Test]
        public async Task GetUserWorkspaces_Unauthorized_ShouldReturnUnauthorizedResponse()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<GetUserWorkspacesResponse> response = await WorkspaceApiClient.GetUserWorkspacesAsync();
            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }

        [Test]
        public async Task GetUserWorkspaces_NoWorkspaces_ShouldReturnEmptyCollection()
        {
            OperationResult<GetUserWorkspacesResponse> response = await WorkspaceApiClient.GetUserWorkspacesAsync();
            Assert.AreEqual(OperationStatus.Success, response.Status);
            Assert.IsEmpty(response.Data.Workspaces);
        }

        [Test]
        public async Task GetUserWorkspaces_WithWorkspaces_ShouldReturnAllWorkspaces()
        {
            IReadOnlyCollection<CreateWorkspaceRequest> createWorkspaceRequests = new[]
            {
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}1", AccessToken = $"{AccessToken}1", GatewayUrl = $"{GatewayUrl}1" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}2", AccessToken = $"{AccessToken}2", GatewayUrl = $"{GatewayUrl}2" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}3", AccessToken = $"{AccessToken}3", GatewayUrl = $"{GatewayUrl}3" },
            };

            foreach (CreateWorkspaceRequest createWorkspaceRequest in createWorkspaceRequests)
            {
                GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(createWorkspaceRequest.GatewayUrl, createWorkspaceRequest.AccessToken)).ReturnsAsync(true);
                await WorkspaceApiClient.CreateAsync(createWorkspaceRequest);
            }

            OperationResult<GetUserWorkspacesResponse> response = await WorkspaceApiClient.GetUserWorkspacesAsync();

            Assert.AreEqual(OperationStatus.Success, response.Status);
            Assert.True(
                response.Data
                    .Workspaces.All(responseModel =>
                        createWorkspaceRequests.Any(
                            requestModel => AreSame(requestModel, responseModel))));
            Assert.True(response.Data.Workspaces.All(workspace => workspace.Id != default));
        }

        private bool AreSame(CreateWorkspaceRequest requestModel, WorkspaceApiModel responseModel)
            => requestModel.Name == responseModel.Name
               && requestModel.GatewayUrl == responseModel.GatewayUrl;
    }
}