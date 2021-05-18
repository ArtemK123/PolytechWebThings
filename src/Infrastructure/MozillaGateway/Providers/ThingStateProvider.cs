using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingStateProvider : IThingStateProvider
    {
        private readonly IGatewayMessageSender gatewayMessageSender;

        public ThingStateProvider(IGatewayMessageSender gatewayMessageSender)
        {
            this.gatewayMessageSender = gatewayMessageSender;
        }

        public async Task<ThingState> GetStateAsync(Thing thing)
        {
            HttpResponseMessage response = await gatewayMessageSender.GetPropertyStatesAsync(thing);
            JsonElement rootElement = JsonDocument.Parse(await response.Content.ReadAsByteArrayAsync()).RootElement;

            Dictionary<Property, string?> propertyStates = new Dictionary<Property, string?>();
            ThingState thingState = new ThingState(thing, propertyStates);

            foreach (var property in thing.Properties)
            {
                JsonElement propertyValueElement = rootElement.GetProperty(property.Name);
                ParsePropertyElement(property, propertyValueElement, propertyStates);
            }

            return thingState;
        }

        private static void ParsePropertyElement(Property property, JsonElement propertyValueElement, Dictionary<Property, string?> propertyStates)
        {
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
    }
}