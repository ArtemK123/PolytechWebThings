using System.Collections.Generic;
using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;
using PolytechWebThings.Infrastructure.MozillaGateway.Resolvers;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators
{
    internal class ThingCreator : IThingCreator
    {
        private readonly IPropertyParserResolver propertyParserResolver;

        public ThingCreator(IPropertyParserResolver propertyParserResolver)
        {
            this.propertyParserResolver = propertyParserResolver;
        }

        public Thing Creator(ThingFlatParsingModel flatThingModel, IWorkspace workspace)
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
                IPropertyCreator creator = propertyParserResolver.Resolve(propertyElement);
                Property property = creator.Create(propertyElement, newThing);
                properties.Add(property);
            }

            return newThing;
        }
    }
}