using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal interface IPropertyParser
    {
        string? PropertyValueType { get; }

        Property Parse(JsonElement propertyJson, Thing thing);
    }
}