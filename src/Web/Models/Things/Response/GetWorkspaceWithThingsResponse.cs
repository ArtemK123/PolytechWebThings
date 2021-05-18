using System.Collections.Generic;
using Web.Models.Workspace.Response;

namespace Web.Models.Things.Response
{
    public record GetWorkspaceWithThingsResponse
    {
        public WorkspaceApiModel? Workspace { get; init; }

        public IReadOnlyCollection<ThingApiModel>? Things { get; init; }
    }
}