using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.GetRuleById
{
    internal class GetRuleByIdHandler : IRequestHandler<GetRuleByIdQuery, Rule>
    {
        private readonly IRuleRepository ruleRepository;
        private readonly ISender mediator;

        public GetRuleByIdHandler(IRuleRepository ruleRepository, ISender mediator)
        {
            this.ruleRepository = ruleRepository;
            this.mediator = mediator;
        }

        public async Task<Rule> Handle(GetRuleByIdQuery request, CancellationToken cancellationToken)
        {
            Rule? rule = await ruleRepository.GetRuleByIdAsync(request.RuleId);
            if (rule is null)
            {
                throw new EntityNotFoundException($"Rule with id={request.RuleId} is not found");
            }

            await CheckUserAccessRights(rule.WorkspaceId, request.UserEmail, cancellationToken);
            return rule;
        }

        private async Task CheckUserAccessRights(int workspaceId, string userEmail, CancellationToken cancellationToken)
        {
            await mediator.Send(new GetWorkspaceByIdQuery(workspaceId, userEmail), cancellationToken);
        }
    }
}