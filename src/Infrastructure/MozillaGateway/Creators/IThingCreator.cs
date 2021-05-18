using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using PolytechWebThings.Infrastructure.MozillaGateway.Models;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators
{
    internal interface IThingCreator
    {
        Thing Creator(ThingFlatParsingModel flatThingModel, IWorkspace workspace);
    }
}