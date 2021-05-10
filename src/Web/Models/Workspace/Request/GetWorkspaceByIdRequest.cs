using Microsoft.AspNetCore.Mvc;

namespace Web.Models.Workspace.Request
{
    [BindProperties]
    public class GetWorkspaceByIdRequest
    {
        [FromRoute(Name = "id")]
        public int? WorkspaceId { get; init; }
    }
}