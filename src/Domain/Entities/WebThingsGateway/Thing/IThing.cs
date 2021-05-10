using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Action;
using Domain.Entities.WebThingsGateway.Property;

namespace Domain.Entities.WebThingsGateway.Thing
{
    public interface IThing
    {
        public string Title { get; }

        public string Href { get; }

        public IReadOnlyCollection<IProperty> Properties { get; }

        public IReadOnlyCollection<IAction> Actions { get; }
    }
}