using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Entities.WebThingsGateway;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal abstract record PropertyParsingModelBase
    {
        public string Name { get; init; }

        public bool Visible { get; init; }

        public string Title { get; init; }

        [JsonPropertyName("type")]
        public string ValueType { get; init; }

        [JsonPropertyName("@type")]
        public string PropertyType { get; init; }

        public IReadOnlyCollection<Link> Links { get; init; }

        public bool ReadOnly { get; init; }
    }
}