using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.GetRuleByWorkspaceAndName
{
    internal class GetRuleByWorkspaceAndNameHandler : IRequestHandler<GetRuleByWorkspaceAndNameQuery, Rule>
    {
        private readonly ISender mediator;
        private readonly IRuleRepository ruleRepository;

        public GetRuleByWorkspaceAndNameHandler(ISender mediator, IRuleRepository ruleRepository)
        {
            this.mediator = mediator;
            this.ruleRepository = ruleRepository;
        }

        public async Task<Rule> Handle(GetRuleByWorkspaceAndNameQuery request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            Rule? rule = await ruleRepository.GetRuleAsync(workspace.Id, request.RuleName);
            if (rule is null)
            {
                throw new EntityNotFoundException($"Can not find rule with name=${request.RuleName} in workspace with name ${workspace.Name}");
            }

            return rule;
        }
    }
}