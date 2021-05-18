using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal class NumberPropertyCreator : PropertyCreatorBase
    {
        public NumberPropertyCreator(IPropertyValueUpdater propertyValueUpdater)
            : base(propertyValueUpdater)
        {
        }

        public override string PropertyValueType => "number";

        public override Property Create(JsonElement propertyJson, Thing thing)
        {
            NumberPropertyParsingModel parsedModel = Deserialize<NumberPropertyParsingModel>(propertyJson);
            return new NumberProperty(
                name: parsedModel.Name,
                visible: parsedModel.Visible,
                title: parsedModel.Title,
                propertyType: parsedModel.PropertyType,
                links: parsedModel.Links,
                readOnly: parsedModel.ReadOnly,
                defaultValue: parsedModel.Value,
                unit: parsedModel.Unit,
                minimum: parsedModel.Minimum,
                maximum: parsedModel.Maximum,
                propertyValueUpdater: PropertyValueUpdater,
                thing);
        }
    }
}