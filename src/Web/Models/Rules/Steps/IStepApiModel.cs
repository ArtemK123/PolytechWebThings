using Domain.Entities.Rule;

namespace Web.Models.Rules.Steps
{
    public interface IStepApiModel
    {
        public int? ExecutionOrderPosition { get; init; }

        public StepType? StepType { get; init; }
    }
}