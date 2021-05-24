using Domain.Entities.Rule;
using MediatR;

namespace Application.Queries.GetRuleById
{
    public record GetRuleByIdQuery : IRequest<Rule>
    {
        public GetRuleByIdQuery(int ruleId, string userEmail)
        {
            RuleId = ruleId;
            UserEmail = userEmail;
        }

        public int RuleId { get; }

        public string UserEmail { get; }
    }
}