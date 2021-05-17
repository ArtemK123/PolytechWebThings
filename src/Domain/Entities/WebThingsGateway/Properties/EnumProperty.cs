using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record EnumProperty : StringProperty
    {
        public EnumProperty(
            string name, bool visible, string title, string propertyType, IReadOnlyCollection<Link> links, bool readOnly, string defaultValue, IReadOnlyCollection<string> allowedValues)
            : base(name, visible, title, propertyType, links, readOnly, defaultValue)
        {
            AllowedValues = allowedValues;
        }

        public override GatewayValueType ValueType => GatewayValueType.Enum;

        public IReadOnlyCollection<string> AllowedValues { get; }

        public override void ValidateValue(string? value)
        {
            throw new System.NotImplementedException();
        }
    }
}