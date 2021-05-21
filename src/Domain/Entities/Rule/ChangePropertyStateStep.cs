namespace Domain.Entities.Rule
{
    public record ChangePropertyStateStep : Step
    {
        public ChangePropertyStateStep(int executionOrderNumber, string? thingId, string? propertyName, string? newPropertyState)
            : base(executionOrderNumber)
        {
            ThingId = thingId;
            PropertyName = propertyName;
            NewPropertyState = newPropertyState;
        }

        public override StepType StepType => StepType.ChangeThingState;

        public string? ThingId { get; }

        public string? PropertyName { get; }

        public string? NewPropertyState { get; }
    }
}