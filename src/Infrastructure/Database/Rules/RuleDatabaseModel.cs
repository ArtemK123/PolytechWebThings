using System.Collections.Generic;
using PolytechWebThings.Infrastructure.Database.Rules.Steps;

namespace PolytechWebThings.Infrastructure.Database.Rules
{
    internal class RuleDatabaseModel
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }

        public string Name { get; set; }

        public List<ChangePropertyStateStepDatabaseModel> ChangePropertyStateSteps { get; set; }

        public List<ExecuteRuleStepDatabaseModel> ExecuteRuleSteps { get; set; }
    }
}