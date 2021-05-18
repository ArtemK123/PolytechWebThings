using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.CommonTestBases
{
    internal abstract class StoredWorkspaceApiTestBase : MockedHttpClientApiTestBase
    {
        protected const string WorkspaceName = "TestName";
        protected const string GatewayUrl = "http://localhost:1214";
        protected const string AccessToken = "j.w.t";

        protected WorkspaceApiClient WorkspaceApiClient { get; private set; }

        protected int WorkspaceId { get; private set; }

        [SetUp]
        public async Task StoredWorkspaceApiTestBaseSetUp()
        {
            WorkspaceApiClient = new WorkspaceApiClient(HttpClient, new HttpResponseMessageParser());
            MockGatewayPingEndpoint();
            await WorkspaceApiClient.CreateAsync(new CreateWorkspaceRequest { Name = WorkspaceName, AccessToken = AccessToken, GatewayUrl = GatewayUrl });
            OperationResult<GetUserWorkspacesResponse> getUserWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesAsync();
            WorkspaceId = getUserWorkspacesResponse.Data.Workspaces.First().Id;
        }

        protected void MockGatewayPingEndpoint(HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, GatewayUrl + "/ping", HttpMethod.Get),
                new HttpResponseMessage(responseStatus));
        }

        protected bool CheckHttpRequestMessage(HttpRequestMessage request, string url, HttpMethod method)
            => request.RequestUri?.AbsoluteUri == url
               && request.Method == method
               && request.Headers.Authorization?.Scheme == "Bearer"
               && request.Headers.Authorization?.Parameter == AccessToken
               && request.Headers.Accept.Single().MediaType == "application/json";
    }
}