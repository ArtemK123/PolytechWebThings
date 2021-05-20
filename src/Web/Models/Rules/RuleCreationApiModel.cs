using System.Collections.Generic;
using Web.Models.Rules.Steps;

namespace Web.Models.Rules
{
    public class RuleCreationApiModel
    {
        public string? RuleName { get; init; }

        public IReadOnlyCollection<StepApiModel>? Steps { get; init; }
    }
}