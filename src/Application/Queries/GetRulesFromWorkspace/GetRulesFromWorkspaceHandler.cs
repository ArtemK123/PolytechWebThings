using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Rule;
using MediatR;

namespace Application.Queries.GetRulesFromWorkspace
{
    internal class GetRulesFromWorkspaceHandler : IRequestHandler<GetRulesFromWorkspaceQuery, IReadOnlyCollection<Rule>>
    {
        private readonly IRuleRepository ruleRepository;
        private readonly ISender mediator;

        public GetRulesFromWorkspaceHandler(IRuleRepository ruleRepository, ISender mediator)
        {
            this.ruleRepository = ruleRepository;
            this.mediator = mediator;
        }

        public async Task<IReadOnlyCollection<Rule>> Handle(GetRulesFromWorkspaceQuery request, CancellationToken cancellationToken)
        {
            await VerifyThatUserHasAccessToWorkspaceAsync(request);
            return await ruleRepository.GetRulesAsync(request.WorkspaceId);
        }

        private async Task VerifyThatUserHasAccessToWorkspaceAsync(GetRulesFromWorkspaceQuery request)
        {
            await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail));
        }
    }
}