using Domain.Entities.Rule;

namespace Web.Models.Rules.Steps
{
    public interface IStepApiModel
    {
        public StepType? StepType { get; init; }
    }
}