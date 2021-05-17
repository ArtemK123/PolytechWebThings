using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal interface IThingParser
    {
        Thing Parse(ThingFlatParsingModel flatThingModel, IWorkspace workspace);
    }
}