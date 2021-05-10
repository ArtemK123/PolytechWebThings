using MediatR;

namespace Application.Commands.DeleteWorkspace
{
    public record DeleteWorkspaceCommand : IRequest
    {
        public DeleteWorkspaceCommand(int workspaceId, string userEmail)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }
    }
}