using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingStateProvider : IThingStateProvider
    {
        private readonly HttpClient httpClient;

        public ThingStateProvider(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(nameof(HttpClient));
        }

        public async Task<ThingState> GetStateAsync(Thing thing)
        {
            string url = thing.Workspace.GatewayUrl + thing.Links.First(link => link.Rel == "properties").Href;
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", thing.Workspace.AccessToken)
                }
            });
            JsonElement rootElement = JsonDocument.Parse(await response.Content.ReadAsByteArrayAsync()).RootElement;

            Dictionary<Property, string?> propertyStates = new Dictionary<Property, string?>();
            ThingState thingState = new ThingState(thing, propertyStates);

            foreach (var property in thing.Properties)
            {
                JsonElement propertyValueElement = rootElement.GetProperty(property.Name);

                if (property.ValueType == GatewayValueType.Boolean)
                {
                    bool state = propertyValueElement.GetBoolean();
                    propertyStates.Add(property, state.ToString().ToLower());
                }
                else if (property.ValueType == GatewayValueType.Number)
                {
                    int state = propertyValueElement.GetInt32();
                    propertyStates.Add(property, state.ToString());
                }
                else if (property.ValueType == GatewayValueType.String || property.ValueType == GatewayValueType.Enum)
                {
                    string? state = propertyValueElement.GetString();
                    propertyStates.Add(property, state);
                }
                else
                {
                    throw new NotSupportedException($"Not supported property value type {property.ValueType}");
                }
            }

            return thingState;
        }
    }
}