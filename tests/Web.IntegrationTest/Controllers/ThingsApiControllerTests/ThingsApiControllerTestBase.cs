using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.CommonTestBases;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests
{
    internal abstract class ThingsApiControllerTestBase : StoredWorkspaceApiTestBase
    {
        protected ThingsApiClient ThingsApiClient { get; private set; }

        [SetUp]
        public void ThingsApiControllerTestBaseSetUp()
        {
            ThingsApiClient = new ThingsApiClient(HttpClient, new HttpResponseMessageParser());
        }

        protected void MockGatewayThingsEndpoint(string returnedContent)
        {
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, GatewayUrl + "/things", HttpMethod.Get),
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(returnedContent) });
        }
    }
}