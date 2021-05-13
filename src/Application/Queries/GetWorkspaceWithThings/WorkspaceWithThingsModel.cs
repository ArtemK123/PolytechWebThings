using System.Collections.Generic;
using Domain.Entities.WebThingsGateway.Thing;
using Domain.Entities.Workspace;

namespace Application.Queries.GetWorkspaceWithThings
{
    public record WorkspaceWithThingsModel
    {
        public WorkspaceWithThingsModel(IWorkspace workspace, IReadOnlyCollection<Thing> things)
        {
            Workspace = workspace;
            Things = things;
        }

        public IWorkspace Workspace { get; }

        public IReadOnlyCollection<Thing> Things { get; }
    }
}