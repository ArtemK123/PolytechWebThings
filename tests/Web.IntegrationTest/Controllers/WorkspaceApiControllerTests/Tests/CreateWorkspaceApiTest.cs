using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Workspace.Request;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class CreateWorkspaceApiTest : WorkspaceApiControllerTestBase
    {
        [Test]
        public async Task Create_InvalidModel_ShouldReturnBadRequestResponse()
        {
            HttpResponseMessage response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            string expectedResponseMessage =
                "{\"Name\":[\"'Name' must not be empty.\"],\"GatewayUrl\":[\"'Gateway Url' must not be empty.\"],\"AccessToken\":[\"'Access Token' must not be empty.\"]}";
            string actualResponseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedResponseMessage, actualResponseMessage);
        }

        [Test]
        public async Task Create_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();

            HttpResponseMessage response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = string.Empty,
                GatewayUrl = string.Empty,
                AccessToken = string.Empty
            });

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Create_GatewayUrlIsAlreadyUsed_ShouldReturnBadRequestResponse()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            var firstRequest = new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            };
            var secondRequest = firstRequest with { Name = "other name", AccessToken = "otherj.w.t" };

            await WorkspaceApiClient.CreateAsync(firstRequest);
            HttpResponseMessage response = await WorkspaceApiClient.CreateAsync(secondRequest);

            string responseMessage = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Gateway is already assigned to workspace. Gateway url is {GatewayUrl}", responseMessage);
        }

        [Test]
        public async Task Create_CannotConnectToGateway_ShouldReturnBadRequestResponse()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(false);
            HttpResponseMessage response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            string responseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Can not connect to gateway using the provided url and access token", responseMessage);
        }

        [Test]
        public async Task Create_Success_ShouldReturnOkResponse()
        {
            GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(GatewayUrl, AccessToken)).ReturnsAsync(true);
            HttpResponseMessage response = await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest
            {
                Name = WorkspaceName,
                GatewayUrl = GatewayUrl,
                AccessToken = AccessToken
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}