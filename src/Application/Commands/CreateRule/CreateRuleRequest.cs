using Domain.Entities.Rule.Models;
using MediatR;

namespace Application.Commands.CreateRule
{
    public record CreateRuleRequest : IRequest
    {
        public CreateRuleRequest(int workspaceId, string usedEmail, RuleCreationModel ruleCreationModel)
        {
            WorkspaceId = workspaceId;
            UsedEmail = usedEmail;
            RuleCreationModel = ruleCreationModel;
        }

        public int WorkspaceId { get; }

        public string UsedEmail { get; }

        public RuleCreationModel RuleCreationModel { get; }
    }
}