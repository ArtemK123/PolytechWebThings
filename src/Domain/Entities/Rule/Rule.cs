using System.Collections.Generic;

namespace Domain.Entities.Rule
{
    public record Rule
    {
        public string Name { get; }

        public IReadOnlyCollection<Step> Steps { get; }
    }
}