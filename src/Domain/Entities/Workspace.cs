using System.Collections.Generic;
using Domain.Entities.Device;

namespace Domain.Entities
{
    public record Workspace
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string GatewayUrl { get; init; }

        public IReadOnlyCollection<UserToWorkspaceConnection> Users { get; init; } = new List<UserToWorkspaceConnection>();

        public IReadOnlyCollection<IDevice> Devices { get; init; } = new List<IDevice>();
    }
}