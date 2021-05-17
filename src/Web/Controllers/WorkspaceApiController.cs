using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateWorkspace;
using Application.Commands.DeleteWorkspace;
using Application.Commands.UpdateWorkspace;
using Application.Converters;
using Application.Queries.GetUserWorkspaces;
using Application.Queries.GetWorkspaceById;
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
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;
using Web.Providers;

namespace Web.Controllers
{
    public class WorkspaceApiController : ControllerBase
    {
        private readonly ISender mediator;
        private readonly IUserEmailProvider userEmailProvider;

        public WorkspaceApiController(ISender mediator, IUserEmailProvider userEmailProvider)
        {
            this.mediator = mediator;
            this.userEmailProvider = userEmailProvider;
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Create([FromBody] CreateWorkspaceRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(
                new CreateWorkspaceCommand(
                    name: NullableConverter.GetOrThrow(request.Name),
                    gatewayUrl: NullableConverter.GetOrThrow(request.GatewayUrl),
                    accessToken: NullableConverter.GetOrThrow(request.AccessToken),
                    userEmail: userEmailProvider.GetUserEmail(HttpContext)),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpGet]
        [Authorize]
        public async Task<OperationResult<GetUserWorkspacesResponse>> GetUserWorkspaces(CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            IReadOnlyCollection<IWorkspace> workspaces = await mediator.Send(new GetUserWorkspacesQuery(userEmail: userEmail), cancellationToken);
            IReadOnlyCollection<WorkspaceApiModel> convertedWorkspaces = workspaces.Select(ConvertApiModel).ToArray();
            return new OperationResult<GetUserWorkspacesResponse>(OperationStatus.Success, new GetUserWorkspacesResponse(convertedWorkspaces));
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<WorkspaceApiModel>> GetById([FromBody]GetWorkspaceByIdRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.Id!.Value, userEmail), cancellationToken);
            return ConvertToOperationResult(workspace);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Update([FromBody]UpdateWorkspaceRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            await mediator.Send(
                new UpdateWorkspaceCommand(
                    workspaceId: NullableConverter.GetOrThrow(request.Id),
                    name: NullableConverter.GetOrThrow(request.Name),
                    gatewayUrl: NullableConverter.GetOrThrow(request.GatewayUrl),
                    accessToken: NullableConverter.GetOrThrow(request.AccessToken),
                    userEmail),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Delete([FromBody]DeleteWorkspaceRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            await mediator.Send(new DeleteWorkspaceCommand(workspaceId: NullableConverter.GetOrThrow(request.Id), userEmail: userEmail), cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThings([FromBody] GetWorkspaceWithThingsRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            WorkspaceWithThingsModel result
                = await mediator.Send(new GetWorkspaceWithThingsQuery(workspaceId: NullableConverter.GetOrThrow(request.WorkspaceId), userEmail), cancellationToken);
            WorkspaceApiModel convertedWorkspaceModel = ConvertApiModel(result.Workspace);
            IReadOnlyCollection<ThingApiModel> convertedThingModels = result.Things.Select(Convert).ToArray();
            return new OperationResult<GetWorkspaceWithThingsResponse>(
                OperationStatus.Success,
                new GetWorkspaceWithThingsResponse { Workspace = convertedWorkspaceModel, Things = convertedThingModels });
        }

        private static OperationResult<WorkspaceApiModel> ConvertToOperationResult(IWorkspace workspace)
            => new OperationResult<WorkspaceApiModel>(
                OperationStatus.Success,
                ConvertApiModel(workspace));

        private static WorkspaceApiModel ConvertApiModel(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);

        private ThingApiModel Convert(Thing thing) => new ThingApiModel { Title = thing.Title, Properties = thing.Properties.Select(Convert).ToArray() };

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
                return propertyApiModel with { DefaultValue = convertedProperty.DefaultValue.ToString() };
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