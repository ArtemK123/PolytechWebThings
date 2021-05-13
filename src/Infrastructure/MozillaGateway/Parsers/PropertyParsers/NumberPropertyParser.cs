using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal class NumberPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "number";

        public override Property Parse(JsonElement propertyJson)
        {
            NumberPropertyParsingModel parsedModel = Deserialize<NumberPropertyParsingModel>(propertyJson);
            return new NumberProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                value: parsedModel.Value,
                unit: parsedModel.Unit,
                minimum: parsedModel.Minimum,
                maximum: parsedModel.Maximum);
        }
    }
}