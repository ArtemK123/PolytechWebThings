namespace Web.Models.Rules.Steps
{
    public record ExecuteRuleStepApiModel : StepApiModel
    {
        public string? RuleName { get; init; }
    }
}