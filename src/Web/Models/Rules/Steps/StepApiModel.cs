using Domain.Entities.Rule;

namespace Web.Models.Rules.Steps
{
    public record StepApiModel : IChangeThingStateStepApiModel, IExecuteRuleStepApiModel
    {
        public int? ExecutionOrderPosition { get; init; }

        public StepType? StepType { get; init; }

        public string? ThingId { get; init; }

        public string? PropertyName { get; init; }

        public string? NewPropertyState { get; init; }

        public string? RuleName { get; init; }
    }
}