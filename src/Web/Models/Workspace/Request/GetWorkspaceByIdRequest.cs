namespace Web.Models.Workspace.Request
{
    public record GetWorkspaceByIdRequest
    {
        public int? Id { get; init; }
    }
}