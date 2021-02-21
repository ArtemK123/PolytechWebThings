using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities.Device
{
    internal class Device : IDevice
    {
        public Device(
            string id,
            string name,
            ConnectionStatus connectionStatus,
            string description,
            string url,
            Workspace workspace,
            IReadOnlyCollection<Function> functions)
        {
            Id = id;
            Name = name;
            ConnectionStatus = connectionStatus;
            Description = description;
            Url = url;
            Workspace = workspace;
            Functions = functions;
        }

        public string Id { get; }

        public string Name { get; }

        public ConnectionStatus ConnectionStatus { get; }

        public string Description { get; }

        public string Url { get; }

        public Workspace Workspace { get; }

        public IReadOnlyCollection<Function> Functions { get; }

        public IDevice ChangeConnectionStatus(ConnectionStatus newStatus)
        {
            return new Device(Id, Name, newStatus, Description, Url, Workspace, Functions);
        }
    }
}