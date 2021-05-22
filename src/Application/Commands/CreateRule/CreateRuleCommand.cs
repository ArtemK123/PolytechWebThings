using Domain.Entities.Rule.Models;
using MediatR;

namespace Application.Commands.CreateRule
{
    public record CreateRuleCommand : IRequest
    {
        public CreateRuleCommand(int workspaceId, string userEmail, RuleCreationModel ruleCreationModel)
        {
            WorkspaceId = workspaceId;
            UserEmail = userEmail;
            RuleCreationModel = ruleCreationModel;
        }

        public int WorkspaceId { get; }

        public string UserEmail { get; }

        public RuleCreationModel RuleCreationModel { get; }
    }
}