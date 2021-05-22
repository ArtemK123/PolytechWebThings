namespace Domain.Entities.Rule.Models
{
    public class ExecuteRuleStepCreationModel : StepCreationModel
    {
        public ExecuteRuleStepCreationModel(StepType stepType)
            : base(stepType)
        {
        }

        public string RuleName { get; }
    }
}