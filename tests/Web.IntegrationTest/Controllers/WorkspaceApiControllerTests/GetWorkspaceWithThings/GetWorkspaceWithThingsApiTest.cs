using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.GetWorkspaceWithThings
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class GetWorkspaceWithThingsApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        [Test]
        public async Task GetWorkspaceWithThings_PrimitivePropertyValues()
        {
            OperationResult<GetWorkspaceWithThingsResponse> result = await WorkspaceApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Success, result.Message);
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
        }
    }
}