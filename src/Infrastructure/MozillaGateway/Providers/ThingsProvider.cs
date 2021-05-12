using System.Collections.Generic;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway.Thing;
using Domain.Entities.Workspace;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingsProvider : IThingsProvider
    {
        public Task<IReadOnlyCollection<IThing>> GetAsync(IWorkspace workspace)
        {
            throw new System.NotImplementedException();
        }
    }
}