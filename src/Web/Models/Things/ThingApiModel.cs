using System.Collections.Generic;

namespace Web.Models.Things
{
    public record ThingApiModel
    {
        public string? Title { get; init;  }

        public IReadOnlyCollection<PropertyApiModel>? Properties { get; init; }
    }
}