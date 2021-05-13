using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Resolvers
{
    internal class PropertyParserResolver : IPropertyParserResolver
    {
        private readonly IEnumerable<IPropertyParser> propertyParsers;

        public PropertyParserResolver(IEnumerable<IPropertyParser> propertyParsers)
        {
            this.propertyParsers = propertyParsers;
        }

        public IPropertyParser Resolve(JsonElement propertyJson)
        {
            if (!propertyJson.TryGetProperty("type", out JsonElement typeElement))
            {
                throw new NotSupportedException("Cannot find type field for property");
            }

            string? propertyValueType = typeElement.GetString();

            IPropertyParser? parser = propertyParsers.SingleOrDefault(currentParser => currentParser.PropertyValueType == propertyValueType);

            if (parser is null)
            {
                throw new NotSupportedException($"Unsupported property value`s type {propertyValueType}");
            }

            return parser;
        }
    }
}