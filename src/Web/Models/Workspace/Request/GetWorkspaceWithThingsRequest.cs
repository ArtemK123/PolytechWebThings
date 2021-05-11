namespace Web.Models.Workspace.Request
{
    public record GetWorkspaceWithThingsRequest
    {
        public int? WorkspaceId { get; init; }
    }
}