using System.Collections.Generic;
using PolytechWebThings.Infrastructure.Database.Rules.Steps;

namespace PolytechWebThings.Infrastructure.Database.Rules
{
    internal class RuleDatabaseModel
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }

        public string Name { get; set; } = null!;

        public List<ChangeThingStateStepDatabaseModel> ChangeThingStateSteps { get; set; } = null!;

        public List<ExecuteRuleStepDatabaseModel> ExecuteRuleSteps { get; set; } = null!;
    }
}