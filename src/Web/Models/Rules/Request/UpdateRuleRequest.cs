using System.Collections.Generic;
using Web.Models.Rules.Steps;

namespace Web.Models.Rules.Request
{
    public record UpdateRuleRequest
    {
        public int? RuleId { get; init; }

        public string? NewRuleName { get; init; }

        public IReadOnlyCollection<StepApiModel?>? UpdatedSteps { get; init; }
    }
}