using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities.Device
{
    public interface IDevice
    {
        public string Id { get; }

        public string Name { get; }

        public ConnectionStatus ConnectionStatus { get; }

        public string Description { get; }

        public string Url { get; }

        public Workspace Workspace { get; }

        public IReadOnlyCollection<Function> Functions { get; }

        public IDevice ChangeConnectionStatus(ConnectionStatus newStatus);
    }
}