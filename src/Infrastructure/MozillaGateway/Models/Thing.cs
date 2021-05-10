using System.Collections.Generic;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Models
{
    public class Thing
    {
        public string Title { get; set; }

        public string Href { get; set; }

        public IReadOnlyCollection<Property> Properties { get; set; }

        public IReadOnlyCollection<Action> Actions { get; set; }
    }
}