using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Converters;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingsProvider : IThingsProvider
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private readonly IReadOnlyCollection<IPropertyParser> propertyParsers;
        private readonly IGatewayMessageSender gatewayMessageSender;

        public ThingsProvider(IEnumerable<IPropertyParser> propertyParsers, IGatewayMessageSender gatewayMessageSender)
        {
            this.gatewayMessageSender = gatewayMessageSender;
            this.propertyParsers = propertyParsers.ToArray();
        }

        public async Task<IReadOnlyCollection<Thing>> GetAsync(IWorkspace workspace)
        {
            try
            {
                HttpResponseMessage response = await gatewayMessageSender.SendGetThingsRequest(workspace);
                string responseText = await response.Content.ReadAsStringAsync();
                IReadOnlyCollection<Thing> parsedThings = Deserialize(responseText);
                return parsedThings;
            }
            catch (Exception exception)
            {
                throw new BrokenGatewayCommunicationException(exception);
            }
        }

        private IReadOnlyCollection<Thing> Deserialize(string serializedText)
        {
            IReadOnlyCollection<ThingFlatParsingModel> flatThingModels
                = NullableConverter.GetOrThrow(JsonSerializer.Deserialize<IReadOnlyCollection<ThingFlatParsingModel>>(serializedText, jsonSerializerOptions));

            IReadOnlyCollection<Thing> parsedThings = flatThingModels.Select(ParseThing).ToList();

            return parsedThings;
        }

        private Thing ParseThing(ThingFlatParsingModel flatThingModel)
        {
            IReadOnlyCollection<Property> parsedProperties = flatThingModel.Properties.Select(keyValuePair => ParseProperty(keyValuePair.Value)).ToArray();

            return new Thing
            {
                Title = flatThingModel.Title,
                Types = flatThingModel.Types,
                Description = flatThingModel.Description,
                Href = flatThingModel.Href,
                SelectedCapability = flatThingModel.SelectedCapability,
                Id = flatThingModel.Id,
                Links = flatThingModel.Links,
                Properties = parsedProperties
            };
        }

        private Property ParseProperty(JsonElement propertyJson)
        {
            if (!propertyJson.TryGetProperty("type", out JsonElement typeElement))
            {
                throw new NotSupportedException("Cannot find type field for property");
            }

            string? propertyValueType = typeElement.GetString();

            IPropertyParser? parser = propertyParsers.SingleOrDefault(currentParser => currentParser.PropertyValueType == propertyValueType);

            if (parser is null)
            {
                throw new NotSupportedException($"Unsupported property value`s type {propertyValueType}");
            }

            return parser.Parse(propertyJson);
        }
    }
}