using Domain.Entities.WebThingsGateway.Things;
using Web.Models.Things;

namespace Web.Converters
{
    public interface IThingApiModelConverter
    {
        ThingApiModel Convert(Thing thing);
    }
}