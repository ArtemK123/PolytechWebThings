using System.Threading.Tasks;
using Domain.Entities.WebThingsGateway.Properties;

namespace Domain.Updaters
{
    public interface IPropertyValueUpdater
    {
        Task UpdateAsync<TValueType>(Property property, TValueType? newValue);
    }
}