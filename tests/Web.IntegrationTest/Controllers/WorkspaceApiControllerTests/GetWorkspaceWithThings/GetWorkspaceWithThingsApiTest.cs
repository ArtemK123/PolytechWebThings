using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task GetWorkspaceWithThings_PrimitivePropertyValues_ShouldDeserializeAndReturnThings()
        {
            string resourcesFolder = Path.GetFullPath("Controllers/WorkspaceApiControllerTests/GetWorkspaceWithThings/PrimitivePropertyValues");
            await SetupHttpMessageHandlerMock(resourcesFolder);
            string serializedExpected = await File.ReadAllTextAsync(Path.Combine(resourcesFolder, "Expected.json"));

            OperationResult<GetWorkspaceWithThingsResponse> result = await WorkspaceApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            string serializedActual = JsonSerializer.Serialize(result.Data);

            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreEqual(serializedExpected, serializedActual);
        }

        private async Task SetupHttpMessageHandlerMock(string resourcesFolder)
        {
            string inputPath = Path.Combine(resourcesFolder, "Input.json");
            string jsonFileContent = await File.ReadAllTextAsync(inputPath);
            SetupHttpMessageHandlerMock(
                IsHttpRequestMessageValid,
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(jsonFileContent) });
        }

        private bool IsHttpRequestMessageValid(HttpRequestMessage httpRequestMessage)
            => httpRequestMessage.RequestUri?.AbsoluteUri == GatewayUrl + "/things"
               && httpRequestMessage.Method == HttpMethod.Get
               && httpRequestMessage.Headers.Authorization?.Scheme == "Bearer"
               && httpRequestMessage.Headers.Authorization?.Parameter == AccessToken
               && httpRequestMessage.Headers.Accept.Single().MediaType == "application/json";
    }
}