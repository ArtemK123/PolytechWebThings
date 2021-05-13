using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class NumberPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "number";

        public override Property Parse(JsonElement propertyJson)
        {
            NumberPropertyParsingModel parsedModel = Deserialize<NumberPropertyParsingModel>(propertyJson);
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
                Minimum = parsedModel.Minimum,
                Maximum = parsedModel.Maximum
            };
        }
    }
}