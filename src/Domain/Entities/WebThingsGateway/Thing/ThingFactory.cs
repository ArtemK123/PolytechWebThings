using Domain.Entities.Common;

namespace Domain.Entities.WebThingsGateway.Thing
{
    internal class ThingFactory : IFactory<ParsedThingCreationModel, IThing>
    {
        public IThing Create(ParsedThingCreationModel creationModel)
        {
            return new Thing(title: creationModel.Title, href: creationModel.Href, properties: creationModel.Properties, actions: creationModel.Actions);
        }
    }
}