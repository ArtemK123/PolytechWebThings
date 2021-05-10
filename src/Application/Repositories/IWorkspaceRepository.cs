using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Workspace;

namespace Application.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<IWorkspace?> GetByGatewayUrlAsync(string url);

        Task<IWorkspace?> GetByIdAsync(int id);

        Task<IReadOnlyCollection<IWorkspace>> GetByUserEmail(string userEmail);

        Task AddAsync(IWorkspace workspace);

        Task UpdateAsync(IWorkspace workspace);

        Task DeleteAsync(IWorkspace workspace);
    }
}