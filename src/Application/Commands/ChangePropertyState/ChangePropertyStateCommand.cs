using MediatR;

namespace Application.Commands.ChangePropertyState
{
    public record ChangePropertyStateCommand : IRequest
    {
        public ChangePropertyStateCommand(int workspaceId, string thingId, string propertyName, string? newPropertyValue, string userEmail)
        {
            WorkspaceId = workspaceId;
            ThingId = thingId;
            PropertyName = propertyName;
            NewPropertyValue = newPropertyValue;
            UserEmail = userEmail;
        }

        public int WorkspaceId { get; }

        public string ThingId { get; }

        public string PropertyName { get; }

        public string? NewPropertyValue { get; }

        public string UserEmail { get; }
    }
}