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
            return new NumberProperty
            {
                Name = parsedModel.Name,
                Visible = parsedModel.Visible,
                Title = parsedModel.Title,
                PropertyType = parsedModel.PropertyType,
                Links = parsedModel.Links,
                ReadOnly = parsedModel.ReadOnly,
                Value = parsedModel.Value,
                Unit = parsedModel.Unit,
                Minimum = int.MinValue,
                Maximum = int.MaxValue
            };
        }
    }
}