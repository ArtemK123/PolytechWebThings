using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Parsers
{
    internal interface IGetThingsResponseParser
    {
        Task<IReadOnlyCollection<Thing>> Parse(HttpResponseMessage response);
    }
}