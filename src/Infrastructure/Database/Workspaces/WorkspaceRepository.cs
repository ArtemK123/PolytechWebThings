using System.Collections.Generic;
using System.Linq;
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

            return Convert(databaseModel);
        }

        public Task<IReadOnlyCollection<IWorkspace>> GetByUserEmail(string userEmail)
        {
            IEnumerable<WorkspaceDatabaseModel> databaseModels = dbContext.Workspaces.Where(workspaceDatabaseModel => workspaceDatabaseModel.UserEmail == userEmail).AsEnumerable();
            IReadOnlyCollection<IWorkspace> workspaces = databaseModels.Select(Convert).ToList();
            return Task.FromResult(workspaces);
        }

        public async Task AddAsync(IWorkspace workspace)
        {
            var databaseModel = new WorkspaceDatabaseModel
            {
                Name = workspace.Name,
                GatewayUrl = workspace.GatewayUrl,
                AccessToken = workspace.AccessToken,
                UserEmail = workspace.UserEmail
            };
            await dbContext.Workspaces.AddAsync(databaseModel);
            await dbContext.SaveChangesAsync();
        }

        private IWorkspace Convert(WorkspaceDatabaseModel databaseModel)
            => storedUserFactory.Create(new StoredWorkspaceCreationModel(
                id: databaseModel.Id,
                name: databaseModel.Name,
                gatewayUrl: databaseModel.GatewayUrl,
                accessToken: databaseModel.AccessToken,
                userEmail: databaseModel.UserEmail));
    }
}