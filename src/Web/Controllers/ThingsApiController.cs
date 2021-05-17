using System.Threading;
using System.Threading.Tasks;
using Application.Commands.ChangePropertyState;
using Application.Converters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
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
    }
}