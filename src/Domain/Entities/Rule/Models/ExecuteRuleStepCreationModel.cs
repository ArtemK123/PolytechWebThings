namespace Domain.Entities.Rule.Models
{
    public class ExecuteRuleStepCreationModel : StepCreationModel
    {
        public ExecuteRuleStepCreationModel(int executionOrder, string ruleName)
            : base(executionOrder, StepType.ExecuteRule)
        {
            RuleName = ruleName;
        }

        public string RuleName { get; }
    }
}