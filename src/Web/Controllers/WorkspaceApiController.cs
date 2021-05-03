using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.CreateWorkspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;

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
    }
}