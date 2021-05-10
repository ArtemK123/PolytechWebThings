using Microsoft.AspNetCore.Mvc;

namespace Web.Models.Workspace.Request
{
    [BindProperties]
    public class DeleteWorkspaceRequest
    {
        [FromRoute(Name = "id")]
        public int? WorkspaceId { get; init; }
    }
}