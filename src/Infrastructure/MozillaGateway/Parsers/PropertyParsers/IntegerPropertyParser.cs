using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal class IntegerPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "integer";

        public override Property Parse(JsonElement propertyJson)
        {
            IntegerPropertyParsingModel parsedModel = Deserialize<IntegerPropertyParsingModel>(propertyJson);
            return new NumberProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                value: parsedModel.Value,
                unit: parsedModel.Unit,
                minimum: int.MinValue,
                maximum: int.MaxValue);
        }
    }
}