namespace Domain.Entities.Rule.Models
{
    public class ExecuteRuleStepCreationModel : StepCreationModel
    {
        public ExecuteRuleStepCreationModel(StepType stepType, int executionOrderPosition, string ruleName)
            : base(stepType, executionOrderPosition)
        {
            RuleName = ruleName;
        }

        public string RuleName { get; }
    }
}