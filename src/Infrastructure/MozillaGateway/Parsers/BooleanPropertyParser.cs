using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class BooleanPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "boolean";

        public override Property Parse(JsonElement propertyJson)
        {
            BooleanPropertyParsingModel parsedModel = Deserialize<BooleanPropertyParsingModel>(propertyJson);
            return new BooleanProperty
            {
                Name = parsedModel.Name,
                Visible = parsedModel.Visible,
                Title = parsedModel.Title,
                PropertyType = parsedModel.PropertyType,
                Links = parsedModel.Links,
                ReadOnly = parsedModel.ReadOnly,
                Value = parsedModel.Value
            };
        }
    }
}