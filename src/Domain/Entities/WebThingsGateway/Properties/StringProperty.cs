namespace Domain.Entities.WebThingsGateway.Properties
{
    public record StringProperty : Property
    {
        public override GatewayValueType ValueType => GatewayValueType.String;

        public string Value { get; init; }
    }
}