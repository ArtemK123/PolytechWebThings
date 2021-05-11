using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal class WorkspaceApiControllerWithStoredWorkspaceTestBase : WorkspaceApiControllerTestBase
    {
        protected CreateWorkspaceRequest StoredWorkspace { get; private set; }

        protected int WorkspaceId { get; private set; }

        [SetUp]
        public async Task WorkspaceApiControllerWithStoredWorkspaceTestBaseSetUp()
        {
            StoredWorkspace = new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl };
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl });
            OperationResult<GetUserWorkspacesResponse> getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesAsync();
            WorkspaceId = getUserWorkspacesResponse.Data.Workspaces.First().Id;
        }
    }
}