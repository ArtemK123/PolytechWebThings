using System.Collections.Generic;
using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
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

        public Thing Parse(ThingFlatParsingModel flatThingModel, IWorkspace workspace)
        {
            List<Property> properties = new List<Property>();

            Thing newThing = new Thing(
                title: flatThingModel.Title,
                types: flatThingModel.Types,
                description: flatThingModel.Description,
                href: flatThingModel.Href,
                selectedCapability: flatThingModel.SelectedCapability,
                id: flatThingModel.Id,
                links: flatThingModel.Links,
                properties: properties,
                workspace);

            foreach (JsonElement propertyElement in flatThingModel.Properties.Values)
            {
                IPropertyParser parser = propertyParserResolver.Resolve(propertyElement);
                Property property = parser.Parse(propertyElement, newThing);
                properties.Add(property);
            }

            return newThing;
        }
    }
}