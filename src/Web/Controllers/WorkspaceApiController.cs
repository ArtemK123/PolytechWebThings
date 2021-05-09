using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.CreateWorkspace;
using Application.Queries.GetUserWorkspaces;
using Domain.Entities.Workspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.Controllers
{
    public class WorkspaceApiController : ControllerBase
    {
        private readonly ISender mediator;

        public WorkspaceApiController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task Create([FromBody] CreateWorkspaceRequest request)
        {
            await mediator.Send(new CreateWorkspaceCommand(
                name: request.Name ?? throw new NullReferenceException(),
                gatewayUrl: request.GatewayUrl ?? throw new NullReferenceException(),
                accessToken: request.AccessToken ?? throw new NullReferenceException(),
                userEmail: User.FindFirstValue(ClaimTypes.Email)));
        }

        [HttpGet]
        [Authorize]
        public async Task<GetUserWorkspacesResponse> GetUserWorkspaces()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            IReadOnlyCollection<IWorkspace> workspaces = await mediator.Send(new GetUserWorkspacesQuery(userEmail: userEmail));
            IReadOnlyCollection<WorkspaceApiModel> convertedWorkspaces = workspaces.Select(workspace => new WorkspaceApiModel(workspace.Id, workspace.Name, workspace.GatewayUrl)).ToArray();
            return new GetUserWorkspacesResponse(convertedWorkspaces);
        }
    }
}