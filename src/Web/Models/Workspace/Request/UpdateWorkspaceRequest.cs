using Microsoft.AspNetCore.Mvc;

namespace Web.Models.Workspace.Request
{
    [BindProperties]
    public record UpdateWorkspaceRequest
    {
        [FromRoute(Name = "id")]
        public int? WorkspaceId { get; init; }

        [FromBody]
        public string? Name { get; init; }

        [FromBody]
        public string? GatewayUrl { get; init; }

        [FromBody]
        public string? AccessToken { get; init; }
    }
}