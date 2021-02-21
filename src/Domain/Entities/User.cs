using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public record User
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string? SessionToken { get; init; }

        public UserRole Role { get; init; }

        public IReadOnlyCollection<UserToWorkspaceConnection> WorkspaceCollections { get; init; } = new List<UserToWorkspaceConnection>();
    }
}