using System.Collections.Generic;

namespace Web.Models.Workspace.Response
{
    public record GetUserWorkspacesResponse
    {
        public GetUserWorkspacesResponse(IReadOnlyCollection<WorkspaceApiModel> workspaces)
        {
            Workspaces = workspaces;
        }

        public IReadOnlyCollection<WorkspaceApiModel> Workspaces { get; }
    }
}