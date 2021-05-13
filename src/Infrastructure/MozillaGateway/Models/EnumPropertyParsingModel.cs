using System.Collections.Generic;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    internal record EnumPropertyParsingModel : PropertyParsingModelBase
    {
        public string Value { get; init; }

        public IReadOnlyCollection<string> Enum { get; init; }
    }
}