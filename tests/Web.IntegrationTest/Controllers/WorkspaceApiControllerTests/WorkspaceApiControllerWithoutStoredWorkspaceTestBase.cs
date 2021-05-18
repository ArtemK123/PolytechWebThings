using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.CommonTestBases;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal abstract class WorkspaceApiControllerWithoutStoredWorkspaceTestBase : MockedHttpClientApiTestBase
    {
        protected const string WorkspaceName = "TestName";
        protected const string GatewayUrl = "http://localhost:1214";
        protected const string AccessToken = "j.w.t";

        protected WorkspaceApiClient WorkspaceApiClient { get; private set; }

        [SetUp]
        public void WorkspaceApiControllerTestBaseSetUp()
        {
            WorkspaceApiClient = new WorkspaceApiClient(HttpClient, new HttpResponseMessageParser());
        }

        protected void MockGatewayConnectionCheck(bool validConnection, string gatewayUrl = GatewayUrl, string accessToken = AccessToken)
        {
            HttpStatusCode responseStatus = validConnection ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;
            SetupHttpMessageHandlerMock(
                request
                    => request.RequestUri?.AbsoluteUri == gatewayUrl + "/ping"
                       && request.Method == HttpMethod.Get
                       && request.Headers.Authorization?.Scheme == "Bearer"
                       && request.Headers.Authorization?.Parameter == accessToken
                       && request.Headers.Accept.Single().MediaType == "application/json",
                new HttpResponseMessage(responseStatus));
        }
    }
}