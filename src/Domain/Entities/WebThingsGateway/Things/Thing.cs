using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.Workspace;

namespace Domain.Entities.WebThingsGateway.Things
{
    public record Thing
    {
        public Thing(
            string title,
            IReadOnlyCollection<string> types,
            string description,
            string href,
            string selectedCapability,
            string id,
            IReadOnlyCollection<Link> links,
            IReadOnlyCollection<Property> properties, IWorkspace workspace)
        {
            Title = title;
            Types = types;
            Description = description;
            Href = href;
            SelectedCapability = selectedCapability;
            Id = id;
            Links = links;
            Properties = properties;
            Workspace = workspace;
        }

        public string Title { get; }

        public IReadOnlyCollection<string> Types { get; }

        public string Description { get; }

        public string Href { get; }

        public string SelectedCapability { get; }

        public string Id { get; }

        public IReadOnlyCollection<Link> Links { get; }

        public IReadOnlyCollection<Property> Properties { get; }

        public IWorkspace Workspace { get; }
    }
}