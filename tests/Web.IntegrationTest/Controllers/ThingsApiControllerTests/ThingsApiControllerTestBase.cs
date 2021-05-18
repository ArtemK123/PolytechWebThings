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

        protected void MockGatewayThingsEndpoint(HttpStatusCode statusCode = HttpStatusCode.OK, string returnedContent = null)
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = statusCode };

            if (returnedContent is not null)
            {
                response.Content = new StringContent(returnedContent);
            }

            SetupHttpMessageHandlerMock(IsGatewayThingsEndpointRequest, response);
        }

        protected void MockGatewayThingsEndpoint(string returnedContent) => MockGatewayThingsEndpoint(HttpStatusCode.OK, returnedContent);

        protected bool IsGatewayThingsEndpointRequest(HttpRequestMessage httpRequestMessage) => CheckHttpRequestMessage(httpRequestMessage, GatewayUrl + "/things", HttpMethod.Get);
    }
}