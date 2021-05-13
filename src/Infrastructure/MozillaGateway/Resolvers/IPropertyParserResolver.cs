using System.Text.Json;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Resolvers
{
    internal interface IPropertyParserResolver
    {
        IPropertyParser Resolve(JsonElement propertyJson);
    }
}