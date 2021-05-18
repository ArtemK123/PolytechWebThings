using System.Text.Json;
using PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Resolvers
{
    internal interface IPropertyParserResolver
    {
        IPropertyCreator Resolve(JsonElement propertyJson);
    }
}