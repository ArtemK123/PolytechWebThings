using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Workspace;

namespace PolytechWebThings.Infrastructure.Database.Workspaces
{
    internal class WorkspaceRepository : IWorkspaceRepository
    {
        public Task<IWorkspace?> GetByGatewayUrlAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(IWorkspace workspace)
        {
            throw new System.NotImplementedException();
        }
    }
}