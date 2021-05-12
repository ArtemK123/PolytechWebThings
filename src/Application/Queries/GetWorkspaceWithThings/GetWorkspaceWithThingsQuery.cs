using MediatR;

namespace Application.Queries.GetWorkspaceWithThings
{
    public record GetWorkspaceWithThingsQuery : IRequest<WorkspaceWithThingsModel>
    {
        public GetWorkspaceWithThingsQuery(int workspaceId, string userEmail)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }
    }
}