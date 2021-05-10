using Microsoft.AspNetCore.Mvc;

namespace Web.Models.Workspace.Request
{
    [BindProperties]
    public record GetWorkspaceByIdRequest
    {
        [FromRoute(Name = "id")]
        public int? WorkspaceId { get; init; }
    }
}