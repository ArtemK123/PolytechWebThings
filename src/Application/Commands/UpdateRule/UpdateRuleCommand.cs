using System.Collections.Generic;
using Domain.Entities.Rule.Models;
using MediatR;

namespace Application.Commands.UpdateRule
{
    public record UpdateRuleCommand : IRequest
    {
        public UpdateRuleCommand(string userEmail, int ruleId, string newRuleName, IReadOnlyCollection<StepModel> updatedSteps)
        {
            UserEmail = userEmail;
            RuleId = ruleId;
            NewRuleName = newRuleName;
            UpdatedSteps = updatedSteps;
        }

        public string UserEmail { get; }

        public int RuleId { get; }

        public string NewRuleName { get; }

        public IReadOnlyCollection<StepModel> UpdatedSteps { get; }
    }
}