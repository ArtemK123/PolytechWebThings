namespace Web.Models.Rules.Request
{
    public record GetRuleByIdRequest
    {
        public int RuleId { get; init; }
    }
}