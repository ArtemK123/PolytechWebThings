using Domain.Enums;

namespace Domain.Entities
{
    public record UserToWorkspaceConnection
    {
        public string Id { get; init; }

        public User User { get; init; }

        public Workspace Workspace { get; init; }

        public WorkspaceAccessRights WorkspaceAccessRights { get; init; }
    }
}