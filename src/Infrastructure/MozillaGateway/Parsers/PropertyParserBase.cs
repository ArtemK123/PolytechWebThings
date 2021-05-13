using System.Text.Json;
using Application.Converters;
using Domain.Entities.WebThingsGateway.Properties;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal abstract class PropertyParserBase : IPropertyParser
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public abstract string PropertyValueType { get; }

        public abstract Property Parse(JsonElement propertyJson);

        protected TParsingModel Deserialize<TParsingModel>(JsonElement json)
            where TParsingModel : PropertyParsingModelBase
            => NullableConverter.GetOrThrow(JsonSerializer.Deserialize<TParsingModel>(json.GetRawText(), jsonSerializerOptions));
    }
}