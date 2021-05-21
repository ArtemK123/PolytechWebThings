namespace Web.Models.Rules.Steps
{
    public interface IExecuteRuleStepApiModel : IStepApiModel
    {
        public string? RuleName { get; init; }
    }
}