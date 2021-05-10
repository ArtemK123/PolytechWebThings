using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CreateWorkspace;
using Application.Commands.DeleteWorkspace;
using Application.Queries.GetUserWorkspaces;
using Application.Queries.GetWorkspaceById;
using Domain.Entities.Workspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Create([FromBody] CreateWorkspaceRequest request)
        {
            await mediator.Send(new CreateWorkspaceCommand(
                name: request.Name ?? throw new NullReferenceException(),
                gatewayUrl: request.GatewayUrl ?? throw new NullReferenceException(),
                accessToken: request.AccessToken ?? throw new NullReferenceException(),
                userEmail: userEmailProvider.GetUserEmail(HttpContext)));
        }

        [HttpGet]
        [Authorize]
        public async Task<GetUserWorkspacesResponse> GetUserWorkspaces()
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            IReadOnlyCollection<IWorkspace> workspaces = await mediator.Send(new GetUserWorkspacesQuery(userEmail: userEmail));
            IReadOnlyCollection<WorkspaceApiModel> convertedWorkspaces = workspaces.Select(Convert).ToArray();
            return new GetUserWorkspacesResponse(convertedWorkspaces);
        }

        [HttpGet]
        [Authorize]
        public async Task<WorkspaceApiModel> GetById(GetWorkspaceByIdRequest request)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId!.Value, userEmail));
            return Convert(workspace);
        }

        [HttpPut]
        [Authorize]
        public async Task Update(UpdateWorkspaceRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Authorize]
        public async Task Delete(DeleteWorkspaceRequest request)
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            await mediator.Send(new DeleteWorkspaceCommand(workspaceId: request.WorkspaceId.GetValueOrDefault(), userEmail: userEmail));
        }

        private static WorkspaceApiModel Convert(IWorkspace workspace)
            => new WorkspaceApiModel(id: workspace.Id, name: workspace.Name, accessToken: workspace.AccessToken, gatewayUrl: workspace.GatewayUrl);
    }
}