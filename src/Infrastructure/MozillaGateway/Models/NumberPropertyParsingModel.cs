namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record NumberPropertyParsingModel : PropertyParsingModelBase
    {
        public int Value { get; init; }

        public string Unit { get; init; }

        public int Minimum { get; init; }

        public int Maximum { get; init; }
    }
}