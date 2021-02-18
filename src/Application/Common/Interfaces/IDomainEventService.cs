using PolytechWebThings.Domain.Common;
using System.Threading.Tasks;

namespace PolytechWebThings.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
