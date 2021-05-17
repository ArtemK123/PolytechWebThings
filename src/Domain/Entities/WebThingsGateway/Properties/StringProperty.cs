using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record StringProperty : Property
    {
        public StringProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, string defaultValue)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            DefaultValue = defaultValue;
        }

        public override GatewayValueType ValueType => GatewayValueType.String;

        public string DefaultValue { get; }

        public override void ValidateValue(string? value)
        {
            throw new System.NotImplementedException();
        }
    }
}