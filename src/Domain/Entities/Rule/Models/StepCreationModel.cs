namespace Domain.Entities.Rule.Models
{
    public abstract class StepCreationModel
    {
        protected StepCreationModel(StepType stepType, int executionOrderPosition)
        {
            StepType = stepType;
            ExecutionOrderPosition = executionOrderPosition;
        }

        public int ExecutionOrderPosition { get; }

        public StepType StepType { get; }
    }
}