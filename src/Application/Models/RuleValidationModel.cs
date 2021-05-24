using System.Collections.Generic;
using Domain.Entities.Rule.Models;

namespace Application.Models
{
    internal record RuleValidationModel
    {
        public RuleValidationModel(string ruleName, IReadOnlyCollection<StepModel> steps)
        {
            RuleName = ruleName;
            Steps = steps;
        }

        public string RuleName { get; }

        public IReadOnlyCollection<StepModel> Steps { get; }
    }
}