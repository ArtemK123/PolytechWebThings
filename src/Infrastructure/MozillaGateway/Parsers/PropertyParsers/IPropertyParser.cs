using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal interface IPropertyParser
    {
        string? PropertyValueType { get; }

        Property Parse(JsonElement propertyJson);
    }
}