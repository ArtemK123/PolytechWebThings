namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record StringPropertyParsingModel : PropertyParsingModelBase
    {
        public string Value { get; init; } = default!;
    }
}