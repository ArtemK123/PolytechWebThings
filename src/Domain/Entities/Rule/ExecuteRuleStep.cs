namespace Domain.Entities.Rule
{
    public record ExecuteRuleStep : Step
    {
        public ExecuteRuleStep(int executionOrderPosition, string ruleName)
            : base(executionOrderPosition)
        {
            RuleName = ruleName;
        }

        public override StepType StepType => StepType.ExecuteRule;

        public string RuleName { get; }
    }
}