using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Entities.Workspace;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal class GatewayMessageSender : IGatewayMessageSender
    {
        private const string ThingsApiAddress = "/things";

        private readonly IHttpClientFactory httpClientFactory;

        public GatewayMessageSender(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> SendGetThingsRequest(IWorkspace workspace)
        {
            string thingsUrl = workspace.GatewayUrl + ThingsApiAddress;
            return await httpClientFactory.CreateClient(nameof(GatewayMessageSender)).SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", workspace.AccessToken)
                }
            });
        }
    }
}