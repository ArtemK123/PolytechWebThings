using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Resolvers;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class ThingParser : IThingParser
    {
        private readonly IPropertyParserResolver propertyParserResolver;

        public ThingParser(IPropertyParserResolver propertyParserResolver)
        {
            this.propertyParserResolver = propertyParserResolver;
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
            IPropertyParser parser = propertyParserResolver.Resolve(propertyJson);
            return parser.Parse(propertyJson);
        }
    }
}