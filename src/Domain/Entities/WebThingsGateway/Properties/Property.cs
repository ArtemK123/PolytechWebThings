using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Updaters;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public abstract record Property
    {
        protected Property(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, IPropertyValueUpdater propertyValueUpdater)
        {
            Name = name;
            Visible = visible;
            Title = title;
            PropertyType = propertyType;
            Links = links;
            ReadOnly = readOnly;
            PropertyValueUpdater = propertyValueUpdater;
        }

        public string Name { get; }

        public bool Visible { get; }

        public string Title { get; }

        public abstract GatewayValueType ValueType { get; }

        public string PropertyType { get; }

        public IReadOnlyCollection<Link> Links { get; }

        public bool ReadOnly { get; }

        protected IPropertyValueUpdater PropertyValueUpdater { get; }

        public abstract Task UpdateValueAsync(string? value);
    }
}