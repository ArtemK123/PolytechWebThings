namespace Domain.Entities.Rule
{
    public record ChangeThingStateStep : Step
    {
        public ChangeThingStateStep(int executionOrderPosition, string thingId, string propertyName, string? newPropertyState)
            : base(executionOrderPosition)
        {
            ThingId = thingId;
            PropertyName = propertyName;
            NewPropertyState = newPropertyState;
        }

        public override StepType StepType => StepType.ChangeThingState;

        public string ThingId { get; }

        public string PropertyName { get; }

        public string? NewPropertyState { get; }
    }
}