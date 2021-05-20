namespace Web.Models.Rules.Request
{
    public class CreateRuleRequest
    {
        public RuleCreationApiModel? RuleCreationModel { get; init; }

        public int? WorkspaceId { get; init; }
    }
}