namespace Domain.Entities.Rule
{
    public record ExecuteRuleStep : Step
    {
        public ExecuteRuleStep(int executionOrderNumber, string ruleName)
            : base(executionOrderNumber)
        {
            RuleName = ruleName;
        }

        public override StepType StepType => StepType.ExecuteRule;

        public string RuleName { get; }
    }
}