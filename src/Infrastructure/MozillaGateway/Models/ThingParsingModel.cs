using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record ThingParsingModel
    {
        public string Title { get; init; }

        [JsonPropertyName("@type")]
        public IReadOnlyCollection<string> Types { get; init; }

        public string Description { get; init; }

        public string Href { get; init; }

        public IReadOnlyDictionary<string, PropertyParsingModel> Properties { get; init; }

        public IReadOnlyCollection<LinkParsingModel> Links { get; init; }

        public string SelectedCapability { get; init; }

        public string Id { get; init; }
    }
}