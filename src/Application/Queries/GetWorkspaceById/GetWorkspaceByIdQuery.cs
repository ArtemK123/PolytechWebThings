using Domain.Entities.Workspace;
using MediatR;

namespace Application.Queries.GetWorkspaceById
{
    public class GetWorkspaceByIdQuery : IRequest<IWorkspace>
    {
        public GetWorkspaceByIdQuery(int workspaceId, string userEmail)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }
    }
}