using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetThingById;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.ChangePropertyState
{
    internal class ChangePropertyStateHandler : IRequestHandler<ChangePropertyStateCommand>
    {
        private readonly ISender mediator;

        public ChangePropertyStateHandler(ISender mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(ChangePropertyStateCommand request, CancellationToken cancellationToken)
        {
            Thing targetThing = await mediator.Send(
                new GetThingByIdQuery(userEmail: request.UserEmail, workspaceId: request.WorkspaceId, thingId: request.ThingId),
                cancellationToken);

            Property? targetProperty = targetThing.Properties.SingleOrDefault(property => property.Name == request.PropertyName);
            if (targetProperty is null)
            {
                throw new EntityNotFoundException($"Can not find property with name={request.PropertyName} in thing {targetThing.Title}");
            }

            await targetProperty.UpdateValueAsync(request.NewPropertyValue);
            return Unit.Value;
        }
    }
}