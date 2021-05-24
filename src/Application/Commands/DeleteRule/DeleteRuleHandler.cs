using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetRuleById;
using Application.Repositories;
using Domain.Entities.Rule;
using MediatR;

namespace Application.Commands.DeleteRule
{
    internal class DeleteRuleHandler : IRequestHandler<DeleteRuleCommand>
    {
        private readonly IRuleRepository ruleRepository;
        private readonly ISender mediator;

        public DeleteRuleHandler(IRuleRepository ruleRepository, ISender mediator)
        {
            this.ruleRepository = ruleRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteRuleCommand request, CancellationToken cancellationToken)
        {
            Rule rule = await mediator.Send(new GetRuleByIdQuery(request.RuleId, request.UserEmail), cancellationToken);
            await ruleRepository.DeleteAsync(rule.Id);
            return Unit.Value;
        }
    }
}