namespace Web.Models.Workspace.Request
{
    public record CreateWorkspaceRequest
    {
        public string? Name { get; init; }

        public string? GatewayUrl { get; init; }

        public string? AccessToken { get; init; }
    }
}