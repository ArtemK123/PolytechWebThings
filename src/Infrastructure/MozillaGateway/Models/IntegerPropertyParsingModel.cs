namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record IntegerPropertyParsingModel : PropertyParsingModelBase
    {
        public int Value { get; init; }

        public string Unit { get; init; }
    }
}