using System.Collections.Generic;
using Domain.Entities.Rule.Models;
using Domain.Entities.Workspace;
using MediatR;

namespace Application.Commands.ValidateRule
{
    internal class ValidateRuleCommand : IRequest
    {
        public ValidateRuleCommand(string ruleName, IWorkspace workspace, IReadOnlyCollection<StepModel> steps)
        {
            RuleName = ruleName;
            Workspace = workspace;
            Steps = steps;
        }

        public string RuleName { get; }

        public IWorkspace Workspace { get; }

        public IReadOnlyCollection<StepModel> Steps { get; }
    }
}