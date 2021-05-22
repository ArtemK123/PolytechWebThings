namespace Domain.Entities.Rule
{
    public abstract record Step
    {
        protected Step(int executionOrderPosition)
        {
            ExecutionOrderPosition = executionOrderPosition;
        }

        public int ExecutionOrderPosition { get; }

        public abstract StepType StepType { get; }
    }
}