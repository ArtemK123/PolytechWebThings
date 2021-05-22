namespace Web.Models.Rules.Request
{
    public record CreateRuleRequest
    {
        public RuleCreationApiModel? RuleCreationModel { get; init; }
    }
}