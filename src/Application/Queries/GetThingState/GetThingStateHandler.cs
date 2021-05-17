using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Queries.GetWorkspaceById;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.GetThingState
{
    internal class GetThingStateHandler : IRequestHandler<GetThingStateQuery, ThingState>
    {
        private readonly ISender mediator;
        private readonly IThingsProvider thingsProvider;
        private readonly IThingStateProvider thingStateProvider;

        public GetThingStateHandler(ISender mediator, IThingsProvider thingsProvider, IThingStateProvider thingStateProvider)
        {
            this.mediator = mediator;
            this.thingsProvider = thingsProvider;
            this.thingStateProvider = thingStateProvider;
        }

        public async Task<ThingState> Handle(GetThingStateQuery request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);
            Thing? thing = things.SingleOrDefault(currentThing => currentThing.Id == request.ThingId);
            if (thing is null)
            {
                throw new EntityNotFoundException($"Can not find property with id=${request.ThingId}");
            }

            return await thingStateProvider.GetStateAsync(thing);
        }
    }
}