using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Updaters;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public abstract record Property
    {
        protected Property(
            string name,
            bool visible,
            string title,
            string propertyType,
            IReadOnlyCollection<Link> links,
            bool readOnly,
            IPropertyValueUpdater propertyValueUpdater,
            Thing thing)
        {
            Name = name;
            Visible = visible;
            Title = title;
            PropertyType = propertyType;
            Links = links;
            ReadOnly = readOnly;
            PropertyValueUpdater = propertyValueUpdater;
            Thing = thing;
        }

        public string Name { get; }

        public bool Visible { get; }

        public string Title { get; }

        public abstract GatewayValueType ValueType { get; }

        public string PropertyType { get; }

        public IReadOnlyCollection<Link> Links { get; }

        public bool ReadOnly { get; }

        public Thing Thing { get; }

        protected IPropertyValueUpdater PropertyValueUpdater { get; }

        public abstract bool IsValidValue(string? value);

        public abstract Task UpdateValueAsync(string? value);
    }
}