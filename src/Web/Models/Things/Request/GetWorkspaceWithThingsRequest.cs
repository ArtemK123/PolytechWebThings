namespace Web.Models.Things.Request
{
    public record GetWorkspaceWithThingsRequest
    {
        public int? WorkspaceId { get; init; }
    }
}