using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Updaters
{
    internal class PropertyValueUpdater : IPropertyValueUpdater
    {
        private const string Quote = "\"";
        private readonly IGatewayMessageSender gatewayMessageSender;

        public PropertyValueUpdater(IGatewayMessageSender gatewayMessageSender)
        {
            this.gatewayMessageSender = gatewayMessageSender;
        }

        public async Task UpdateAsync(Property property, bool? newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + newValue?.ToString().ToLower() + "}";
            HttpResponseMessage response = await gatewayMessageSender.UpdatePropertyStateAsync(property, gatewayRequestBody);
            ThrowIfInvalid(response);
        }

        public async Task UpdateAsync(Property property, string? newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + Quote + newValue + Quote + "}";
            HttpResponseMessage response = await gatewayMessageSender.UpdatePropertyStateAsync(property, gatewayRequestBody);
            ThrowIfInvalid(response: response);
        }

        public async Task UpdateAsync(Property property, int? newValue)
        {
            string gatewayRequestBody = "{" + Quote + property.Name + Quote + ":" + newValue + "}";
            HttpResponseMessage response = await gatewayMessageSender.UpdatePropertyStateAsync(property, gatewayRequestBody);
            ThrowIfInvalid(response);
        }

        private static void ThrowIfInvalid(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new NotSupportedException("Not supported response from gateway");
            }
        }
    }
}