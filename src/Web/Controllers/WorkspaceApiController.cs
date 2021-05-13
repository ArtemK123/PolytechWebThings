using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateWorkspace;
using Application.Commands.DeleteWorkspace;
using Application.Commands.UpdateWorkspace;
using Application.Queries.GetUserWorkspaces;
using Application.Queries.GetWorkspaceById;
using Application.Queries.GetWorkspaceWithThings;
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
                    name: ConvertToNonNullable(request.Name),
                    gatewayUrl: ConvertToNonNullable(request.GatewayUrl),
                    accessToken: ConvertToNonNullable(request.AccessToken),
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
                    workspaceId: ConvertToNonNullableValueType(request.Id),
                    name: ConvertToNonNullable(request.Name),
                    gatewayUrl: ConvertToNonNullable(request.GatewayUrl),
                    accessToken: ConvertToNonNullable(request.AccessToken),
                    userEmail),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Delete([FromBody]DeleteWorkspaceRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            await mediator.Send(new DeleteWorkspaceCommand(workspaceId: ConvertToNonNullableValueType(request.Id), userEmail: userEmail), cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<GetWorkspaceWithThingsResponse>> GetWorkspaceWithThings([FromBody] GetWorkspaceWithThingsRequest request, CancellationToken cancellationToken)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            WorkspaceWithThingsModel result
                = await mediator.Send(new GetWorkspaceWithThingsQuery(workspaceId: ConvertToNonNullableValueType(request.WorkspaceId), userEmail), cancellationToken);
            WorkspaceApiModel convertedWorkspaceModel = ConvertApiModel(result.Workspace);
            IReadOnlyCollection<ThingApiModel> convertedThingModels = result.Things.Select(Convert).ToArray();
            return new OperationResult<GetWorkspaceWithThingsResponse>(OperationStatus.Success, new GetWorkspaceWithThingsResponse(convertedWorkspaceModel, convertedThingModels));
        }

        private static OperationResult<WorkspaceApiModel> ConvertToOperationResult(IWorkspace workspace)
            => new OperationResult<WorkspaceApiModel>(
                OperationStatus.Success,
                ConvertApiModel(workspace));

        private static WorkspaceApiModel ConvertApiModel(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);

        private TModel ConvertToNonNullable<TModel>(TModel? nullableModel)
            where TModel : class
        {
            return nullableModel ?? throw new NullReferenceException();
        }

        private TValueType ConvertToNonNullableValueType<TValueType>(TValueType? nullableValueType)
            where TValueType : struct
        {
            return nullableValueType ?? throw new NullReferenceException();
        }

        private ThingApiModel Convert(Thing thing) => new ThingApiModel();
    }
}