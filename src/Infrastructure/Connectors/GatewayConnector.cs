using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Connectors;

namespace PolytechWebThings.Infrastructure.Connectors
{
    internal class GatewayConnector : IGatewayConnector
    {
        private readonly HttpClient httpClient;

        public GatewayConnector(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<bool> CanConnectToGatewayAsync(string gatewayUrl, string accessToken)
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, gatewayUrl)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
                }
            });

            return response.IsSuccessStatusCode;
        }
    }
}