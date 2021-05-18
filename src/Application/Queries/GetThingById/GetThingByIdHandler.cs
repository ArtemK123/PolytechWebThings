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

namespace Application.Queries.GetThingById
{
    internal class GetThingByIdHandler : IRequestHandler<GetThingByIdQuery, Thing>
    {
        private readonly ISender mediator;
        private readonly IThingsProvider thingsProvider;

        public GetThingByIdHandler(ISender mediator, IThingsProvider thingsProvider)
        {
            this.mediator = mediator;
            this.thingsProvider = thingsProvider;
        }

        public async Task<Thing> Handle(GetThingByIdQuery request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);
            Thing? thing = things.SingleOrDefault(currentThing => currentThing.Id == request.ThingId);
            if (thing is null)
            {
                throw new EntityNotFoundException($"Can not find thing with id={request.ThingId}");
            }

            return thing;
        }
    }
}