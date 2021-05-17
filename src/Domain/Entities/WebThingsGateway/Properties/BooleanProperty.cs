using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record BooleanProperty : Property
    {
        public BooleanProperty(string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, bool defaultValue)
            : base(name, visible, title, propertyType, links, readOnly)
        {
            DefaultValue = defaultValue;
        }

        public override GatewayValueType ValueType => GatewayValueType.Boolean;

        public bool DefaultValue { get; }

        public override void ValidateValue(string? value)
        {
            throw new System.NotImplementedException();
        }
    }
}