using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public abstract record Property
    {
        protected Property(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly)
        {
            Name = name;
            Visible = visible;
            Title = title;
            PropertyType = propertyType;
            Links = links;
            ReadOnly = readOnly;
        }

        public string Name { get; }

        public bool Visible { get; }

        public string Title { get; }

        public abstract GatewayValueType ValueType { get; }

        public string PropertyType { get; }

        public IReadOnlyCollection<Link> Links { get; }

        public bool ReadOnly { get; }
    }
}