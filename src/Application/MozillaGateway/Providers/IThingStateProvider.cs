using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Things;

namespace Application.MozillaGateway.Providers
{
    public interface IThingStateProvider
    {
        Task<ThingState> GetStateAsync(Thing thing);
    }
}