using System.Collections.Generic;
using Web.Models.Rules.Steps;

namespace Web.Models.Rules
{
    public class RuleApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WorkspaceId { get; set; }

        public IReadOnlyCollection<StepApiModel> Steps { get; set; }
    }
}