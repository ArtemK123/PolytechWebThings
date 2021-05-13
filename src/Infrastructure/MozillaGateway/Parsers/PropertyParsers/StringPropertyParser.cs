using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
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
            return new StringProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                value: parsedModel.Value);
        }

        private Property ParseEnum(JsonElement propertyJson)
        {
            EnumPropertyParsingModel parsedModel = Deserialize<EnumPropertyParsingModel>(propertyJson);
            return new EnumProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                value: parsedModel.Value,
                allowedValues: parsedModel.Enum);
        }
    }
}