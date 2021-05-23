using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway;
using NUnit.Framework;

namespace Web.IntegrationTest.Controllers.CommonTestBases.MockedGatewayThingsApi
{
    internal abstract class MockedGatewayThingsApiTestBase : StoredWorkspaceApiTestBase
    {
        protected const string PropertyName = "on";
        protected const string NewPropertyState = "false";
        protected const string ThingId = "http://localhost:1214/things/virtual-things-0";
        protected const GatewayValueType PropertyValueType = GatewayValueType.Boolean;
        private const string ThingsInputPath = "Controllers/CommonTestBases/MockedGatewayThingsApi/things.json";

        [SetUp]
        public async Task MockedGatewayThingsApiTestBaseSetUp()
        {
            await SetupThingsEndpointMockAsync(GatewayUrl);
        }

        protected async Task SetupThingsEndpointMockAsync(string gatewayUrl)
        {
            string thingsInput = await ReadContentFromDiskAsync(ThingsInputPath);
            SetupHttpMessageHandlerMock(
                request => CheckHttpRequestMessage(request, gatewayUrl + "/things", HttpMethod.Get),
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(thingsInput) });
        }
    }
}