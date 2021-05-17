using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Properties;

namespace Domain.Entities.WebThingsGateway.Things
{
    public record ThingState
    {
        public ThingState(Thing thing, IReadOnlyDictionary<Property, string> propertyStates)
        {
            Thing = thing;
            PropertyStates = propertyStates;
        }

        public Thing Thing { get; }

        public IReadOnlyDictionary<Property, string> PropertyStates { get; }
    }
}