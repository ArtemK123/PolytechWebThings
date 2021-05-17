using System.Collections.Generic;

namespace Web.Models.Things
{
    public record ThingStateApiModel
    {
        public string? ThingId { get; init; }

        public IReadOnlyDictionary<string, string> PropertyStates { get; init; } = new Dictionary<string, string>();
    }
}