using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Updaters;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Updaters
{
    internal class PropertyValueUpdater : IPropertyValueUpdater
    {
        private const string Quote = "\"";
        private readonly HttpClient httpClient;

        public PropertyValueUpdater(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(nameof(PropertyValueUpdater));
        }

        public async Task UpdateAsync(Property property, bool newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + newValue.ToString().ToLower() + "}";
            string requestUrl = property.Thing.Workspace.GatewayUrl + property.Links.Single().Href;
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", property.Thing.Workspace.AccessToken)
                },
                Content = new StringContent(gatewayRequestBody)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            });
            if (!response.IsSuccessStatusCode)
            {
                throw new NotSupportedException("Not supported response from gateway");
            }
        }

        public async Task UpdateAsync(Property property, string newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + Quote + newValue + Quote + "}";
            string requestUrl = property.Thing.Workspace.GatewayUrl + property.Links.Single().Href;
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", property.Thing.Workspace.AccessToken)
                },
                Content = new StringContent(gatewayRequestBody)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            });
            if (!response.IsSuccessStatusCode)
            {
                throw new NotSupportedException("Not supported response from gateway");
            }
        }

        public async Task UpdateAsync(Property property, int newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + newValue + "}";
            string requestUrl = property.Thing.Workspace.GatewayUrl + property.Links.Single().Href;
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", property.Thing.Workspace.AccessToken)
                },
                Content = new StringContent(gatewayRequestBody)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            });
            if (!response.IsSuccessStatusCode)
            {
                throw new NotSupportedException("Not supported response from gateway");
            }
        }
    }
}