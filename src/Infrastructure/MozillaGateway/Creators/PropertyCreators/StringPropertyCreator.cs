using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal class StringPropertyCreator : PropertyCreatorBase
    {
        public StringPropertyCreator(IPropertyValueUpdater propertyValueUpdater)
            : base(propertyValueUpdater)
        {
        }

        public override string PropertyValueType => "string";

        public override Property Create(JsonElement propertyJson, Thing thing)
        {
            bool isEnum = propertyJson.TryGetProperty("enum", out _);
            return isEnum ? ParseEnum(propertyJson, thing) : ParseString(propertyJson, thing);
        }

        private Property ParseString(JsonElement propertyJson, Thing thing)
        {
            StringPropertyParsingModel parsedModel = Deserialize<StringPropertyParsingModel>(propertyJson);
            return new StringProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                defaultValue: parsedModel.Value,
                propertyValueUpdater: PropertyValueUpdater,
                thing: thing);
        }

        private Property ParseEnum(JsonElement propertyJson, Thing thing)
        {
            EnumPropertyParsingModel parsedModel = Deserialize<EnumPropertyParsingModel>(propertyJson);
            return new EnumProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                defaultValue: parsedModel.Value,
                allowedValues: parsedModel.Enum,
                propertyValueUpdater: PropertyValueUpdater,
                thing: thing);
        }
    }
}