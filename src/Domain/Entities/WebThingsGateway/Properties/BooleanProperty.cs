using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record BooleanProperty : Property
    {
        public BooleanProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, bool value)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            Value = value;
        }

        public override GatewayValueType ValueType => GatewayValueType.Boolean;

        public bool Value { get; }
    }
}