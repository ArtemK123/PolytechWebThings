using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal class StringPropertyParser : PropertyParserBase
    {
        public override string PropertyValueType => "string";

        public override Property Parse(JsonElement propertyJson)
        {
            bool isEnum = propertyJson.TryGetProperty("enum", out _);
            return isEnum ? ParseEnum(propertyJson: propertyJson) : ParseString(propertyJson: propertyJson);
        }

        private Property ParseString(JsonElement propertyJson)
        {
            StringPropertyParsingModel parsedModel = Deserialize<StringPropertyParsingModel>(propertyJson);
            return new EnumProperty
            {
                Name = parsedModel.Name,
                Visible = parsedModel.Visible,
                Title = parsedModel.Title,
                PropertyType = parsedModel.PropertyType,
                Links = parsedModel.Links,
                ReadOnly = parsedModel.ReadOnly,
                Value = parsedModel.Value,
            };
        }

        private Property ParseEnum(JsonElement propertyJson)
        {
            EnumPropertyParsingModel parsedModel = Deserialize<EnumPropertyParsingModel>(propertyJson);
            return new EnumProperty
            {
                Name = parsedModel.Name,
                Visible = parsedModel.Visible,
                Title = parsedModel.Title,
                PropertyType = parsedModel.PropertyType,
                Links = parsedModel.Links,
                ReadOnly = parsedModel.ReadOnly,
                Value = parsedModel.Value,
                AllowedValues = parsedModel.Enum
            };
        }
    }
}