using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Properties;

namespace Domain.Entities.WebThingsGateway.Things
{
    public record Thing
    {
        public string Title { get; init; }

        public IReadOnlyCollection<string> Types { get; init; }

        public string Description { get; init; }

        public string Href { get; init; }

        public string SelectedCapability { get; init; }

        public string Id { get; init; }

        public IReadOnlyCollection<Link> Links { get; init; }

        public IReadOnlyCollection<Property> Properties { get; init; }
    }
}