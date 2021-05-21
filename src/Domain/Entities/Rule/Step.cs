namespace Domain.Entities.Rule
{
    public abstract record Step
    {
        public abstract StepType StepType { get; }
    }
}