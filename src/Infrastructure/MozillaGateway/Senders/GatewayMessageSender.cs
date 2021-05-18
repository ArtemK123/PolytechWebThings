using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Senders
{
    internal class GatewayMessageSender : IGatewayMessageSender
    {
        private readonly HttpClient httpClient;

        public GatewayMessageSender(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(nameof(GatewayMessageSender));
        }

        public async Task<HttpResponseMessage> GetThingsAsync(string gatewayBaseUrl, string accessToken)
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

        public async Task<HttpResponseMessage> PingAsync(string gatewayBaseUrl, string accessToken)
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

        public async Task<HttpResponseMessage> UpdatePropertyStateAsync(Property property, string serializedPayloadWithNewValue)
        {
            string requestUrl = property.Thing.Workspace.GatewayUrl + property.Links.Single().Href;
            return await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", property.Thing.Workspace.AccessToken),
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                },
                Content = new StringContent(serializedPayloadWithNewValue)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            });
        }
    }
}