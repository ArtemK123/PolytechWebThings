using System.Collections.Generic;

namespace Domain.Entities.WebThingsGateway.Properties
{
    public record EnumProperty : StringProperty
    {
        public override GatewayValueType ValueType => GatewayValueType.Enum;

        public IReadOnlyCollection<string> AllowedValues { get; init; }
    }
}