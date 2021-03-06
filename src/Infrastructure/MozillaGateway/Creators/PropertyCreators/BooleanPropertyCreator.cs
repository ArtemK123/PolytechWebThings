﻿using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal class BooleanPropertyCreator : PropertyCreatorBase
    {
        public BooleanPropertyCreator(IPropertyValueUpdater propertyValueUpdater)
            : base(propertyValueUpdater)
        {
        }

        public override string PropertyValueType => "boolean";

        public override Property Create(JsonElement propertyJson, Thing thing)
        {
            BooleanPropertyParsingModel parsedModel = Deserialize<BooleanPropertyParsingModel>(propertyJson);
            return new BooleanProperty(
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
    }
}