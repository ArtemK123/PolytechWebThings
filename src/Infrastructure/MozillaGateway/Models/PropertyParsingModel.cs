using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record PropertyParsingModel
    {
        public string Name { get; init; }

        public string Value { get; init; }

        public bool Visible { get; init; }

        public string Title { get; init; }

        [JsonPropertyName("type")]
        public string ValueType { get; init; }

        [JsonPropertyName("@type")]
        public string PropertyType { get; init; }

        public string Unit { get; init; }

        public int Minimum { get; init; }

        public int Maximum { get; init; }

        public bool ReadOnly { get; init; }

        public IReadOnlyCollection<LinkParsingModel> Links { get; init; }
    }
}