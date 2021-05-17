using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal interface IGetThingsResponseParser
    {
        Task<IReadOnlyCollection<Thing>> Parse(IWorkspace workspace, HttpResponseMessage response);
    }
}