using System.Collections.Generic;
using Web.Models.Things;

namespace Web.Models.Workspace.Response
{
    public record GetWorkspaceWithThingsResponse
    {
        public GetWorkspaceWithThingsResponse(WorkspaceApiModel workspace, IReadOnlyCollection<ThingApiModel> things)
        {
            Workspace = workspace;
            Things = things;
        }

        public WorkspaceApiModel Workspace { get; init; }

        public IReadOnlyCollection<ThingApiModel> Things { get; init; }
    }
}