using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal class IntegerPropertyParser : PropertyParserBase
    {
        public IntegerPropertyParser(IPropertyValueUpdater propertyValueUpdater)
            : base(propertyValueUpdater)
        {
        }

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
                defaultValue: parsedModel.Value,
                unit: parsedModel.Unit,
                minimum: int.MinValue,
                maximum: int.MaxValue,
                propertyValueUpdater: PropertyValueUpdater);
        }
    }
}