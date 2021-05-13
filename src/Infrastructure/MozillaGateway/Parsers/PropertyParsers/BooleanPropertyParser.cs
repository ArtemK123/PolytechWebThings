using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal class BooleanPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "boolean";

        public override Property Parse(JsonElement propertyJson)
        {
            BooleanPropertyParsingModel parsedModel = Deserialize<BooleanPropertyParsingModel>(propertyJson);
            return new BooleanProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                value: parsedModel.Value);
        }
    }
}