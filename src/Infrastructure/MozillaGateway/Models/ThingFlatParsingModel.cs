using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities.WebThingsGateway;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
     internal class ThingFlatParsingModel
     {
         public string Title { get; init; }

         [JsonPropertyName("@type")]
         public IReadOnlyCollection<string> Types { get; init; }

         public string Description { get; init; }

         public string Href { get; init; }

         public string SelectedCapability { get; init; }

         public string Id { get; init; }

         public IReadOnlyCollection<Link> Links { get; init; }

         public IReadOnlyDictionary<string, JsonElement> Properties { get; init; }
     }
}