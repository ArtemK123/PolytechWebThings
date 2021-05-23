using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Exceptions;
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
            Rule? rule = await ruleRepository.GetRuleByIdAsync(request.RuleId);
            if (rule is null)
            {
                throw new EntityNotFoundException($"Can not find rule with id={request.RuleId}");
            }

            await CheckUserAccessRights(rule.WorkspaceId, request.UserEmail, cancellationToken);
            await ruleRepository.DeleteAsync(rule.Id);
            return Unit.Value;
        }

        private async Task CheckUserAccessRights(int workspaceId, string userEmail, CancellationToken cancellationToken)
        {
            await mediator.Send(new GetWorkspaceByIdQuery(workspaceId, userEmail), cancellationToken);
        }
    }
}