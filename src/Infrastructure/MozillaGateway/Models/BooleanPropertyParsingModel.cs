namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record BooleanPropertyParsingModel : PropertyParsingModelBase
    {
        public bool Value { get; init; }
    }
}