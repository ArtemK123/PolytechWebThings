using System.Threading.Tasks;
using Domain.Entities.Workspace;

namespace Application.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<IWorkspace?> GetByGatewayUrlAsync(string url);

        Task AddAsync(IWorkspace workspace);
    }
}