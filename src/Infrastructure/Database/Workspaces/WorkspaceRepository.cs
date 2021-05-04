using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Common;
using Domain.Entities.Workspace;
using Microsoft.EntityFrameworkCore;

namespace PolytechWebThings.Infrastructure.Database.Workspaces
{
    internal class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFactory<StoredWorkspaceCreationModel, IWorkspace> storedUserFactory;

        public WorkspaceRepository(ApplicationDbContext dbContext, IFactory<StoredWorkspaceCreationModel, IWorkspace> storedUserFactory)
        {
            this.dbContext = dbContext;
            this.storedUserFactory = storedUserFactory;
        }

        public async Task<IWorkspace?> GetByGatewayUrlAsync(string url)
        {
            WorkspaceDatabaseModel? databaseModel = await dbContext.Workspaces.SingleOrDefaultAsync(model => model.GatewayUrl == url);
            if (databaseModel is null)
            {
                return null;
            }

            return storedUserFactory.Create(new StoredWorkspaceCreationModel(
                id: databaseModel.Id,
                name: databaseModel.Name,
                gatewayUrl: databaseModel.GatewayUrl,
                accessToken: databaseModel.AccessToken,
                userEmail: databaseModel.UserEmail));
        }

        public async Task AddAsync(IWorkspace workspace)
        {
            var databaseModel = new WorkspaceDatabaseModel()
            {
                Id = workspace.Id,
                Name = workspace.Name,
                GatewayUrl = workspace.GatewayUrl,
                AccessToken = workspace.AccessToken,
                UserEmail = workspace.UserEmail
            };
            await dbContext.Workspaces.AddAsync(databaseModel);
            await dbContext.SaveChangesAsync();
        }
    }
}