using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal class GatewayMessageSender : IGatewayMessageSender
    {
        private readonly HttpClient httpClient;

        public GatewayMessageSender(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(nameof(GatewayMessageSender));
        }

        public async Task<HttpResponseMessage> SendGetThingsRequestAsync(string gatewayBaseUrl, string accessToken)
        {
            string thingsUrl = gatewayBaseUrl + "/things";
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, thingsUrl)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            });
        }

        public async Task<HttpResponseMessage> SendPingRequestAsync(string gatewayBaseUrl, string accessToken)
        {
            string url = gatewayBaseUrl + "/ping";
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            });
        }
    }
}