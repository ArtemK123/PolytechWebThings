using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public abstract record Property
    {
        public string Name { get; init; }

        public bool Visible { get; init; }

        public string Title { get; init; }

        public abstract GatewayValueType ValueType { get; }

        public string PropertyType { get; init; }

        public IReadOnlyCollection<Link> Links { get; init; }

        public bool ReadOnly { get; init; }
    }
}