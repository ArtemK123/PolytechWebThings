using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Entities.WebThingsGateway;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal abstract record PropertyParsingModelBase
    {
        public string Name { get; init; } = default!;

        public bool Visible { get; init; }

        public string Title { get; init; } = default!;

        [JsonPropertyName("type")]
        public string ValueType { get; init; } = default!;

        [JsonPropertyName("@type")]
        public string PropertyType { get; init; } = default!;

        public IReadOnlyCollection<Link> Links { get; init; } = default!;

        public bool ReadOnly { get; init; }
    }
}