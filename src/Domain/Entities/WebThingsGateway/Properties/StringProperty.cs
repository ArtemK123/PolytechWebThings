using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record StringProperty : Property
    {
        public StringProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, string value)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            Value = value;
        }

        public override GatewayValueType ValueType => GatewayValueType.String;

        public string Value { get; }
    }
}