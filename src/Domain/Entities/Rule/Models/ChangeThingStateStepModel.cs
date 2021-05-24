namespace Domain.Entities.Rule.Models
{
    public class ChangeThingStateStepModel : StepModel
    {
        public ChangeThingStateStepModel(StepType stepType, int executionOrderPosition, string thingId, string propertyName, string? newPropertyState)
            : base(stepType, executionOrderPosition)
        {
            ThingId = thingId;
            PropertyName = propertyName;
            NewPropertyState = newPropertyState;
        }

        public string ThingId { get; }

        public string PropertyName { get; }

        public string? NewPropertyState { get; }
    }
}