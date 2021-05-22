namespace Domain.Entities.Rule.Models
{
    public class ChangePropertyStateStepCreationModel : StepCreationModel
    {
        public ChangePropertyStateStepCreationModel(StepType stepType, string thingId, string propertyName, string? newPropertyState)
            : base(stepType)
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