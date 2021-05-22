namespace Domain.Entities.Rule.Models
{
    public abstract class StepCreationModel
    {
        protected StepCreationModel(StepType stepType)
        {
            StepType = stepType;
        }

        public StepType StepType { get; }
    }
}