using System.Collections.Generic;
using Web.Models.Rules.Steps;

namespace Web.Models.Rules
{
    public record RuleCreationApiModel
    {
        public int? WorkspaceId { get; init; }

        public string? RuleName { get; init; }

        public IReadOnlyCollection<StepApiModel>? Steps { get; init; }
    }
}