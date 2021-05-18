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
using Domain.Entities.Workspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.Controllers
{
    public class WorkspaceApiController : ApiControllerBase
    {
        public WorkspaceApiController(ISender mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Create([FromBody] CreateWorkspaceRequest request, CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new CreateWorkspaceCommand(
                    name: NullableConverter.GetOrThrow(request.Name),
                    gatewayUrl: NullableConverter.GetOrThrow(request.GatewayUrl),
                    accessToken: NullableConverter.GetOrThrow(request.AccessToken),
                    userEmail: UserEmail),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpGet]
        [Authorize]
        public async Task<OperationResult<GetUserWorkspacesResponse>> GetUserWorkspaces(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<IWorkspace> workspaces = await Mediator.Send(new GetUserWorkspacesQuery(userEmail: UserEmail), cancellationToken);
            IReadOnlyCollection<WorkspaceApiModel> convertedWorkspaces = workspaces.Select(ConvertApiModel).ToArray();
            return new OperationResult<GetUserWorkspacesResponse>(OperationStatus.Success, new GetUserWorkspacesResponse(convertedWorkspaces));
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<WorkspaceApiModel>> GetById([FromBody]GetWorkspaceByIdRequest request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await Mediator.Send(new GetWorkspaceByIdQuery(request.Id!.Value, UserEmail), cancellationToken);
            return ConvertToOperationResult(workspace);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Update([FromBody]UpdateWorkspaceRequest request, CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new UpdateWorkspaceCommand(
                    workspaceId: NullableConverter.GetOrThrow(request.Id),
                    name: NullableConverter.GetOrThrow(request.Name),
                    gatewayUrl: NullableConverter.GetOrThrow(request.GatewayUrl),
                    accessToken: NullableConverter.GetOrThrow(request.AccessToken),
                    UserEmail),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Delete([FromBody]DeleteWorkspaceRequest request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteWorkspaceCommand(workspaceId: NullableConverter.GetOrThrow(request.Id), userEmail: UserEmail), cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        private static OperationResult<WorkspaceApiModel> ConvertToOperationResult(IWorkspace workspace)
            => new OperationResult<WorkspaceApiModel>(
                OperationStatus.Success,
                ConvertApiModel(workspace));

        private static WorkspaceApiModel ConvertApiModel(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);
    }
}