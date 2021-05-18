using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal class IntegerPropertyCreator : PropertyCreatorBase
    {
        public IntegerPropertyCreator(IPropertyValueUpdater propertyValueUpdater)
            : base(propertyValueUpdater)
        {
        }

        public override string PropertyValueType => "integer";

        public override Property Create(JsonElement propertyJson, Thing thing)
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
                propertyValueUpdater: PropertyValueUpdater,
                thing: thing);
        }
    }
}