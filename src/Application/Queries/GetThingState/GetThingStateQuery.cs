using Domain.Entities.WebThingsGateway.Things;
using MediatR;

namespace Application.Queries.GetThingState
{
    public record GetThingStateQuery : IRequest<ThingState>
    {
        public GetThingStateQuery(string thingId, int workspaceId, string userEmail)
        {
            ThingId = thingId;
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
        }

        public string ThingId { get; }

        public int WorkspaceId { get; }

        public string UserEmail { get; }
    }
}