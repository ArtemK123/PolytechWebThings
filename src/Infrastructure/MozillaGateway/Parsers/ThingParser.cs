using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class ThingParser : IThingParser
    {
        private readonly IEnumerable<IPropertyParser> propertyParsers;

        public ThingParser(IEnumerable<IPropertyParser> propertyParsers)
        {
            this.propertyParsers = propertyParsers;
        }

        public Thing Parse(ThingFlatParsingModel flatThingModel)
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