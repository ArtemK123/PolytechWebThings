namespace Domain.Entities.Rule
{
    public abstract record Step
    {
        protected Step(int executionOrderNumber)
        {
            ExecutionOrderNumber = executionOrderNumber;
        }

        public int ExecutionOrderNumber { get; }

        public abstract StepType StepType { get; }
    }
}