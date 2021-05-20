using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;

namespace Web.Controllers
{
    public class RulesApiController : ApiControllerBase
    {
        public RulesApiController(ISender mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        [Authorize]
        public Task<OperationResult<CreateRuleResponse>> Create([FromBody] CreateRuleRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}