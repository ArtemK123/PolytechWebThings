namespace Domain.Entities.Rule.Models
{
    public abstract class StepCreationModel
    {
        protected StepCreationModel(int executionOrder, StepType stepType)
        {
            ExecutionOrder = executionOrder;
            StepType = stepType;
        }

        public int ExecutionOrder { get; }

        public StepType StepType { get; }
    }
}