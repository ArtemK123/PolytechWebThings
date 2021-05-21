namespace Domain.Entities.Rule.Models
{
    public class ChangePropertyStateStepCreationModel : StepCreationModel
    {
        public ChangePropertyStateStepCreationModel(int executionOrder, string thingId, string propertyName, string newPropertyState)
            : base(executionOrder, StepType.ChangeThingState)
        {
            ThingId = thingId;
            PropertyName = propertyName;
            NewPropertyState = newPropertyState;
        }

        public string ThingId { get; }

        public string PropertyName { get; }

        public string NewPropertyState { get; }
    }
}