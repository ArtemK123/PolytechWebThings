using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Queries.GetThingById;
using Domain.Entities.WebThingsGateway.Things;
using MediatR;

namespace Application.Queries.GetThingState
{
    internal class GetThingStateHandler : IRequestHandler<GetThingStateQuery, ThingState>
    {
        private readonly ISender mediator;
        private readonly IThingStateProvider thingStateProvider;

        public GetThingStateHandler(ISender mediator, IThingStateProvider thingStateProvider)
        {
            this.mediator = mediator;
            this.thingStateProvider = thingStateProvider;
        }

        public async Task<ThingState> Handle(GetThingStateQuery request, CancellationToken cancellationToken)
        {
            Thing thing = await mediator.Send(
                new GetThingByIdQuery(userEmail: request.UserEmail, workspaceId: request.WorkspaceId, thingId: request.ThingId),
                cancellationToken);

            return await thingStateProvider.GetStateAsync(thing);
        }
    }
}