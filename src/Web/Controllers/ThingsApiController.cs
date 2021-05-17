using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Things.Request;

namespace Web.Controllers
{
    public class ThingsApiController : ControllerBase
    {
        private readonly ISender mediator;

        public ThingsApiController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public Task<OperationResult> ChangePropertyState([FromBody] ChangePropertyStateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}