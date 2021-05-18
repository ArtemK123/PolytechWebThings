using System.Text.Json;
using Application.Converters;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal abstract class PropertyCreatorBase : IPropertyCreator
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        protected PropertyCreatorBase(IPropertyValueUpdater propertyValueUpdater)
        {
            PropertyValueUpdater = propertyValueUpdater;
        }

        public abstract string PropertyValueType { get; }

        protected IPropertyValueUpdater PropertyValueUpdater { get; }

        public abstract Property Create(JsonElement propertyJson, Thing thing);

        protected TParsingModel Deserialize<TParsingModel>(JsonElement json)
            where TParsingModel : PropertyParsingModelBase
            => NullableConverter.GetOrThrow(JsonSerializer.Deserialize<TParsingModel>(json.GetRawText(), jsonSerializerOptions));
    }
}