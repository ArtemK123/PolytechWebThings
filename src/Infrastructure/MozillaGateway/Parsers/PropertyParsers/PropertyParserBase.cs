using System.Text.Json;
using Application.Converters;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Updaters;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers
{
    internal abstract class PropertyParserBase : IPropertyParser
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        protected PropertyParserBase(IPropertyValueUpdater propertyValueUpdater)
        {
            PropertyValueUpdater = propertyValueUpdater;
        }

        public abstract string PropertyValueType { get; }

        protected IPropertyValueUpdater PropertyValueUpdater { get; }

        public abstract Property Parse(JsonElement propertyJson);

        protected TParsingModel Deserialize<TParsingModel>(JsonElement json)
            where TParsingModel : PropertyParsingModelBase
            => NullableConverter.GetOrThrow(JsonSerializer.Deserialize<TParsingModel>(json.GetRawText(), jsonSerializerOptions));
    }
}