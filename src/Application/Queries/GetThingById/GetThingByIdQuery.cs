using Domain.Entities.WebThingsGateway.Things;
using MediatR;

namespace Application.Queries.GetThingById
{
    public class GetThingByIdQuery : IRequest<Thing>
    {
        public GetThingByIdQuery(string userEmail, int workspaceId, string thingId)
        {
            UserEmail = userEmail;
            WorkspaceId = workspaceId;
            ThingId = thingId;
        }

        public string UserEmail { get; }

        public int WorkspaceId { get; }

        public string ThingId { get; }
    }
}