namespace Web.Models.Rules.Steps
{
    public abstract record StepApiModel
    {
        public StepType? StepType { get; init; }
    }
}