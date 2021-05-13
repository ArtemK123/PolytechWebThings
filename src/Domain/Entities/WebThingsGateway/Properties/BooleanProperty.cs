namespace Domain.Entities.WebThingsGateway.Properties
{
    public record BooleanProperty : Property
    {
        public override GatewayValueType ValueType => GatewayValueType.Boolean;

        public bool Value { get; init; }
    }
}