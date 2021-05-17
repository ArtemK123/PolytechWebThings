using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Queries.GetWorkspaceById;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using Domain.Updaters;
using MediatR;

namespace Application.Commands.ChangePropertyState
{
    internal class ChangePropertyStateHandler : IRequestHandler<ChangePropertyStateCommand>
    {
        private readonly ISender mediator;
        private readonly IThingsProvider thingsProvider;
        private readonly IPropertyValueUpdater propertyValueUpdater;

        public ChangePropertyStateHandler(ISender mediator, IThingsProvider thingsProvider, IPropertyValueUpdater propertyValueUpdater)
        {
            this.mediator = mediator;
            this.thingsProvider = thingsProvider;
            this.propertyValueUpdater = propertyValueUpdater;
        }

        public async Task<Unit> Handle(ChangePropertyStateCommand request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);
            Thing? targetThing = things.SingleOrDefault(thing => thing.Id == request.ThingId);
            if (targetThing is null)
            {
                throw new EntityNotFoundException($"Can not find entity with id=${request.ThingId}");
            }

            Property? targetProperty = targetThing.Properties.SingleOrDefault(property => property.Name == request.PropertyName);
            if (targetProperty is null)
            {
                throw new EntityNotFoundException($"Can not find property with name=${request.PropertyName} in thing ${targetThing.Title}");
            }

            await targetProperty.UpdateValueAsync(request.NewPropertyValue);
            return Unit.Value;
        }
    }
}