using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.ChangePropertyState;
using Application.Converters;
using Application.Queries.GetThingState;
using Application.Queries.GetWorkspaceWithThings;
using Domain.Entities.WebThingsGateway;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Things;
using Web.Models.Things.Request;
using Web.Models.Things.Response;
using Web.Models.Workspace.Response;

namespace Web.Controllers
{
    public class ThingsApiController : ApiControllerBase
    {
        public ThingsApiController(ISender mediator)
            : base(mediator)
        {
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
            IReadOnlyCollection<ThingApiModel> convertedThingModels = result.Things.Select(Convert).ToArray();
            return new OperationResult<GetWorkspaceWithThingsResponse>(
                OperationStatus.Success,
                new GetWorkspaceWithThingsResponse { Workspace = convertedWorkspaceModel, Things = convertedThingModels });
        }

        private static WorkspaceApiModel ConvertApiModel(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);

        private ThingApiModel Convert(Thing thing) => new ThingApiModel { Id = thing.Id, Title = thing.Title, Properties = thing.Properties.Select(Convert).ToArray() };

        private PropertyApiModel Convert(Property property)
        {
            var propertyApiModel = new PropertyApiModel
            {
                Name = property.Name,
                Visible = property.Visible,
                Title = property.Title,
                ValueType = property.ValueType,
                PropertyType = property.PropertyType,
                Links = property.Links.Select(Convert).ToArray(),
                ReadOnly = property.ReadOnly
            };

            if (property.ValueType == GatewayValueType.Boolean)
            {
                BooleanProperty convertedProperty = (BooleanProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue.ToString().ToLower() };
            }

            if (property.ValueType == GatewayValueType.Number)
            {
                NumberProperty convertedProperty = (NumberProperty)property;
                return propertyApiModel with
                {
                    DefaultValue = convertedProperty.DefaultValue.ToString(),
                    Unit = convertedProperty.Unit,
                    Minimum = convertedProperty.Minimum,
                    Maximum = convertedProperty.Maximum
                };
            }

            if (property.ValueType == GatewayValueType.String)
            {
                StringProperty convertedProperty = (StringProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue };
            }

            if (property.ValueType == GatewayValueType.Enum)
            {
                EnumProperty convertedProperty = (EnumProperty)property;
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue, AllowedValues = convertedProperty.AllowedValues };
            }

            throw new NotSupportedException("Unsupported property type");
        }

        private LinkApiModel Convert(Link link) => new LinkApiModel
        {
            Rel = link.Rel,
            Href = link.Href
        };
    }
}