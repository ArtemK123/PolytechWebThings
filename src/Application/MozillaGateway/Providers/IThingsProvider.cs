using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Thing;
using Domain.Entities.Workspace;

namespace Application.MozillaGateway.Providers
{
    public interface IThingsProvider
    {
        Task<IReadOnlyCollection<Thing>> GetAsync(IWorkspace workspace);
    }
}