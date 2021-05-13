namespace Domain.Entities.WebThingsGateway.Properties
{
    public record NumberProperty : Property
    {
        public override GatewayValueType ValueType => GatewayValueType.Number;

        public int Value { get; init; }

        public string Unit { get; init; }

        public int Minimum { get; init; }

        public int Maximum { get; init; }
    }
}