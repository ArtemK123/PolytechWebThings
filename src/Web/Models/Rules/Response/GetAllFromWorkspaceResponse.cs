using System.Collections.Generic;

namespace Web.Models.Rules.Response
{
    public class GetAllFromWorkspaceResponse
    {
        public IReadOnlyCollection<RuleApiModel>? Rules { get; init; }
    }
}