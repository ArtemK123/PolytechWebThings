using System.Text.Json;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators
{
    internal interface IPropertyCreator
    {
        string? PropertyValueType { get; }

        Property Create(JsonElement propertyJson, Thing thing);
    }
}