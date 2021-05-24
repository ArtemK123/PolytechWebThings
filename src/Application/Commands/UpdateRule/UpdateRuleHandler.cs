using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetRuleById;
using Domain.Entities.Rule;
using MediatR;

namespace Application.Commands.UpdateRule
{
    internal class UpdateRuleHandler : IRequestHandler<UpdateRuleCommand>
    {
        private readonly ISender mediator;

        public UpdateRuleHandler(ISender mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateRuleCommand request, CancellationToken cancellationToken)
        {
            Rule rule = await mediator.Send(new GetRuleByIdQuery(request.RuleId, request.UserEmail), cancellationToken);
            throw new NotImplementedException();
        }
    }
}