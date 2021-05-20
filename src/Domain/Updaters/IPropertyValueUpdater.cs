using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;

namespace Domain.Updaters
{
    public interface IPropertyValueUpdater
    {
        Task UpdateAsync(Property property, bool? newValue);

        Task UpdateAsync(Property property, string? newValue);

        Task UpdateAsync(Property property, int? newValue);
    }
}