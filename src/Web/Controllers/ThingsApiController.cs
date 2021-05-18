using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.ChangePropertyState;
using Application.Converters;
using Application.Queries.GetThingState;
using Application.Queries.GetWorkspaceWithThings;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Converters;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Models.Things.Response;
using Web.Models.Workspace.Response;

namespace Web.Controllers
{
    public class ThingsApiController : ApiControllerBase
    {
        private readonly IThingApiModelConverter thingApiModelConverter;

        public ThingsApiController(ISender mediator, IThingApiModelConverter thingApiModelConverter)
            : base(mediator)
        {
            this.thingApiModelConverter = thingApiModelConverter;
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> ChangePropertyState([FromBody] ChangePropertyStateRequest request, CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new ChangePropertyStateCommand(
                    workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId),
                    thingId: NullableConverter.GetOrThrow(request.ThingId),
                    propertyName: NullableConverter.GetOrThrow(request.PropertyName),
                    newPropertyValue: request.NewPropertyValue,
                    userEmail: UserEmail),
                cancellationToken);

            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<ThingStateApiModel>> GetThingState([FromBody] GetThingStateRequest request, CancellationToken cancellationToken)
        {
            ThingState thingState = await Mediator.Send(
                new GetThingStateQuery(
                    thingId: NullableConverter.GetOrThrow(request.ThingId),
                    workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId),
                    userEmail: UserEmail),
                cancellationToken);

            return new OperationResult<ThingStateApiModel>(
                OperationStatus.Success,
                new ThingStateApiModel
                {
                    ThingId = thingState.Thing.Id,
                    PropertyStates = thingState.PropertyStates.ToDictionary(pair => pair.Key.Name, pair => pair.Value)
                });
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThings([FromBody] GetWorkspaceWithThingsRequest request, CancellationToken cancellationToken)
        {
            WorkspaceWithThingsModel result
                = await Mediator.Send(new GetWorkspaceWithThingsQuery(workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId), UserEmail), cancellationToken);
            WorkspaceApiModel convertedWorkspaceModel = ConvertApiModel(result.Workspace);
            IReadOnlyCollection<ThingApiModel> convertedThingModels = result.Things.Select(thingApiModelConverter.Convert).ToArray();
            return new OperationResult<GetWorkspaceWithThingsResponse>(
                OperationStatus.Success,
                new GetWorkspaceWithThingsResponse { Workspace = convertedWorkspaceModel, Things = convertedThingModels });
        }

        private static WorkspaceApiModel ConvertApiModel(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);
    }
}