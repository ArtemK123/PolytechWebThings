namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record LinkParsingModel
    {
        public string Rel { get; init; }

        public string Href { get; init; }
    }
}