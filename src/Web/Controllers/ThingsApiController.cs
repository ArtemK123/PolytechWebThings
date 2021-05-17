using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.ChangePropertyState;
using Application.Converters;
using Application.Queries.GetThingState;
using Domain.Entities.WebThingsGateway.Things;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Providers;

namespace Web.Controllers
{
    public class ThingsApiController : ControllerBase
    {
        private readonly ISender mediator;
        private readonly IUserEmailProvider userEmailProvider;

        public ThingsApiController(ISender mediator, IUserEmailProvider userEmailProvider)
        {
            this.mediator = mediator;
            this.userEmailProvider = userEmailProvider;
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> ChangePropertyState([FromBody] ChangePropertyStateRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(
                new ChangePropertyStateCommand(
                    workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId),
                    thingId: NullableConverter.GetOrThrow(request.ThingId),
                    propertyName: NullableConverter.GetOrThrow(request.PropertyName),
                    newPropertyValue: request.NewPropertyValue,
                    userEmail: userEmailProvider.GetUserEmail(HttpContext)),
                cancellationToken);

            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<ThingStateApiModel>> GetThingState([FromBody] GetThingStateRequest request, CancellationToken cancellationToken)
        {
            ThingState thingState = await mediator.Send(
                new GetThingStateQuery(
                    thingId: NullableConverter.GetOrThrow(request.ThingId),
                    workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId),
                    userEmail: userEmailProvider.GetUserEmail(HttpContext)),
                cancellationToken);

            return new OperationResult<ThingStateApiModel>(
                OperationStatus.Success,
                new ThingStateApiModel
                {
                    ThingId = thingState.Thing.Id,
                    PropertyStates = thingState.PropertyStates.ToDictionary(pair => pair.Key.Name, pair => pair.Value)
                });
        }
    }
}