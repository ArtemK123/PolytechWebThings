using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Resolvers
{
    internal class PropertyParserResolver : IPropertyParserResolver
    {
        private readonly IEnumerable<IPropertyCreator> propertyParsers;

        public PropertyParserResolver(IEnumerable<IPropertyCreator> propertyParsers)
        {
            this.propertyParsers = propertyParsers;
        }

        public IPropertyCreator Resolve(JsonElement propertyJson)
        {
            if (!propertyJson.TryGetProperty("type", out JsonElement typeElement))
            {
                throw new NotSupportedException("Cannot find type field for property");
            }

            string? propertyValueType = typeElement.GetString();

            IPropertyCreator? parser = propertyParsers.SingleOrDefault(currentParser => currentParser.PropertyValueType == propertyValueType);

            if (parser is null)
            {
                throw new NotSupportedException($"Unsupported property value`s type {propertyValueType}");
            }

            return parser;
        }
    }
}