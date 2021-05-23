namespace Web.Models.Rules.Request
{
    public record GetAllFromWorkspaceRequest
    {
        public int? WorkspaceId { get; init; }
    }
}