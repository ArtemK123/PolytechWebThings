using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Thing;
using Domain.Entities.Workspace;

namespace Application.Queries.GetWorkspaceWithThings
{
    public record WorkspaceWithThingsModel
    {
        public WorkspaceWithThingsModel(IWorkspace workspace, IReadOnlyCollection<IThing> things)
        {
            Workspace = workspace;
            Things = things;
        }

        public IWorkspace Workspace { get; }

        public IReadOnlyCollection<IThing> Things { get; }
    }
}