using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities.WebThingsGateway;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
     internal class ThingFlatParsingModel
     {
         public string Title { get; init; } = default!;

         [JsonPropertyName("@type")]
         public IReadOnlyCollection<string> Types { get; init; } = default!;

         public string Description { get; init; } = default!;

         public string Href { get; init; } = default!;

         public string SelectedCapability { get; init; } = default!;

         public string Id { get; init; } = default!;

         public IReadOnlyCollection<Link> Links { get; init; } = default!;

         public IReadOnlyDictionary<string, JsonElement> Properties { get; init; } = default!;
     }
}