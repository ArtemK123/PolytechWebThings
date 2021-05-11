namespace Web.Models.Workspace.Request
{
    public record UpdateWorkspaceRequest
    {
        public int? Id { get; init; }

        public string? Name { get; init; }

        public string? GatewayUrl { get; init; }

        public string? AccessToken { get; init; }
    }
}