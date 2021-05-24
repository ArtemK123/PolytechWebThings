namespace Domain.Entities.Rule.Models
{
    public abstract class StepModel
    {
        protected StepModel(StepType stepType, int executionOrderPosition)
        {
            StepType = stepType;
            ExecutionOrderPosition = executionOrderPosition;
        }

        public int ExecutionOrderPosition { get; }

        public StepType StepType { get; }
    }
}