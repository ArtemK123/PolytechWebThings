using Domain.Entities.Rule;
using MediatR;

namespace Application.Queries.GetRuleByWorkspaceAndName
{
    public class GetRuleByWorkspaceAndNameQuery : IRequest<Rule>
    {
        public GetRuleByWorkspaceAndNameQuery(int workspaceId, string userEmail, string ruleName)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
            RuleName = ruleName;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }

        public string RuleName { get; }
    }
}