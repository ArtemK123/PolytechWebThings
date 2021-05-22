namespace Domain.Entities.Rule.Models
{
    public class ExecuteRuleStepCreationModel : StepCreationModel
    {
        public ExecuteRuleStepCreationModel(StepType stepType, string ruleName)
            : base(stepType)
        {
            RuleName = ruleName;
        }

        public string RuleName { get; }
    }
}