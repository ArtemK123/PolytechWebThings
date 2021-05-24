namespace Domain.Entities.Rule.Models
{
    public class ExecuteRuleStepModel : StepModel
    {
        public ExecuteRuleStepModel(StepType stepType, int executionOrderPosition, string ruleName)
            : base(stepType, executionOrderPosition)
        {
            RuleName = ruleName;
        }

        public string RuleName { get; }
    }
}