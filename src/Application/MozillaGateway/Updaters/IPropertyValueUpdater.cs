using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;

namespace Application.MozillaGateway.Updaters
{
    public interface IPropertyValueUpdater
    {
        Task Update(Property property, string? newValue);
    }
}